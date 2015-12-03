using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatsAggregator.Models
{
    public class Log_Universal
    {
        public int Id { get; set; }
        public EntityType EntityType { get; set; }
        public ActionType ActionType { get; set; }
        public int SubscriberId { get; set; }
        public DateTime Datestamp { get; set; }
        public int EntityKey { get; set; }
    }
    public class Log_CommunityMember
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
    }
    public class CommunityMember
    {
        public int Id { get; set; }
        public int SubscriberId { get; set; }
        public int CommunityId { get; set; }
        public DateTime DateAdded { get; set; }
    }
    public enum EntityType
    {
        CommunityMember
    }
    public enum ActionType
    {
        Created,        
        Updated,
        Deleted
    }

    public enum StatValueType
    {
        MembersJoinedCommunity,
        MembersLeftCommunity,
        MembersGained
    }

    public class StatsAggregated
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }        
        public StatValueType StatValueType { get; set; }
        public int Value { get; set; }
        public int? CommunityId { get; set; }
    }
}