using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using StatsAggregator.Models;

namespace StatsAggregator.EF
{
    public class StatsContext : DbContext
    {
        public StatsContext() : base("StatsContext"){}

        public DbSet<Log_Universal> Log_Universal { get; set; }
        public DbSet<Log_CommunityMember> Log_CommunityMember{ get; set; }
        public DbSet<CommunityMember> CommunityMember { get; set; }
        public DbSet<StatsAggregated> StatsAggregated { get; set; }
    }
}