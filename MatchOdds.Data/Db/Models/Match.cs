using MatchOdds.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MatchOdds.Data.Db.Models
{
    public class Match : BaseEntity
    {
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public SportType Sport { get; set; }
        public List<MatchOdds> MatchOdds { get; set; }

    }
}
