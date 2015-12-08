using StatsAggregator.EF;
using StatsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace StatsAggregator.Services
{
    public class FilterModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? CommunityId { get; set; }
    }
    public class StatsAgg
    {
        private StatsContext _db;
        public StatsAgg()
        {
            _db = new StatsContext();
        }

        public void AddTestAggData()
        {
            var d1 = DateTime.Now.AddYears(-10);
            var d2 = DateTime.Now;
            var r = new Random();
            for (var i = d1; i <= d2; i = i.AddDays(1))
            {
                
                var randomInt = r.Next(1, 100);
                var agg = new StatsAggregated()
                {
                    CommunityId = 1,
                    Date = i,
                    StatValueType = StatValueType.MembersJoinedCommunity,
                    Value = randomInt
                };
                Debug.WriteLine($"adding {i.ToShortDateString()} - {randomInt}");
                _db.StatsAggregated.Add(agg);
            }
            _db.SaveChanges();
        }

        public void GetDataByYear()
        {
            var q = _db.StatsAggregated.GroupBy(s => s.Date.Year, s => s.Value, (key,val) => new { Year = key, Count = val.Sum(v => v) }).ToList();

        }
        public void GetDataByMonth()
        {
            var q = _db.StatsAggregated.GroupBy(s => DbFunctions.CreateDateTime( s.Date.Year, s.Date.Month, 1,0,0,0))
                .Select((val) => new { Key = val.Key.Value, Count = val.Sum(v => v.Value) }).OrderBy(r => r.Key).ToList();

        }
        public void GetDataByDay()
        {
            var q = _db.StatsAggregated.GroupBy(s => DbFunctions.CreateDateTime(s.Date.Year, s.Date.Month, s.Date.Day, 0, 0, 0))
                .Select((val) => new { Key = val.Key, Count = val.Sum(v => v.Value) }).OrderBy(r => r.Key).ToList();

        }
        public void ProcessStatsFromBeginningOfTime()
        {
            /* For each community
            For each stat type, 
                get earliest occurance of the value being updated and record datetime
                store the end of period for this aggregation (ie. 1 hour later)
                Retieve log files for this stat over this time period
                Aggregate the values
                Store in database
            */

        }
        public void ProcessStatsForPreviousHour()
        {
            //for all members added regardless of commuity, just dont filter on comunityid

                
        }
        public void Test()
        {
            var filter = new FilterModel() { CommunityId = 1, FromDate = DateTime.Now.AddMinutes(-10), ToDate = DateTime.Now.AddDays(10) };
            var res = CommunityMembersAdded(filter);
        }
        public Dictionary<DateTime,int> CommunityMembersAdded(FilterModel filter)
        {
            var result = _db.Log_Universal
                .Where(u => u.EntityType == EntityType.CommunityMember
                    && u.ActionType == ActionType.Created
                    && u.Datestamp >= filter.FromDate && u.Datestamp <= filter.ToDate)
                .Join(_db.Log_CommunityMember.Where(cm => cm.CommunityId == filter.CommunityId || !filter.CommunityId.HasValue), u => u.EntityKey, c => c.Id, (u, c) => new { u, c })
                .GroupBy(g => DbFunctions.TruncateTime(g.u.Datestamp).Value)
                .Select(g => new { Date = g.Key, Count = g.Select(v => v.u.SubscriberId).Distinct().Count() }).ToDictionary(r => r.Date, r => r.Count);
            return result;
        }
        public void MembersAddedJourney(FilterModel filter)
        {
            var result = _db.Log_Universal.Where(u => u.EntityType == EntityType.CommunityMember)
                .Join(_db.Log_CommunityMember, u => u.EntityKey, c => c.Id, (u, c) => new { u, c })
                .GroupBy(v => v.u.SubscriberId).AsEnumerable()
                .Select(g => new { Group = g, Count = g.Count() })
                .SelectMany(groupWithCount =>
                    groupWithCount.Group.Select(b => b)
                    .Zip(
                        Enumerable.Range(1, groupWithCount.Count).Reverse(),
                        (j, i) => new { j.u.SubscriberId, j.u.ActionType, i }))
                .Where(r => r.i == 1 && r.ActionType == ActionType.Created)
                .Join(_db.Log_Universal
                    .Where(u => u.EntityType == EntityType.Journey && u.ActionType == ActionType.Created),
                        c => c.SubscriberId, u => u.SubscriberId, (c, u) => new { c, u }
                    )
                .GroupBy(g => g.u.Datestamp.Date)
                .Select(g => new { Date = g.Key, Count = g.Select(v => v.u.SubscriberId).Distinct().Count() })
                .ToDictionary(r => r.Date, r => r.Count);
                
        }
    }   
}