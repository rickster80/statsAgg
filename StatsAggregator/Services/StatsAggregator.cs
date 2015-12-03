using StatsAggregator.EF;
using StatsAggregator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class StatsAggregator
    {
        private StatsContext _db;
        public StatsAggregator()
        {
            _db = new StatsContext();
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
    }
}