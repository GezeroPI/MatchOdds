using MatchOdds.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchOdds.Data.Db
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options) : base(options)
        {
        }

        public DbSet<Models.Match> Match { get; set; }
        public DbSet<Models.MatchOdds> MatchOdds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Match>().
                Property(p => p.Sport)
                .HasConversion<int>();
        }
    }
}
