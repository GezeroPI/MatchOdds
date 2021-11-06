using System;
using System.Collections.Generic;
using System.Text;

namespace MatchOdds.Data.Db.Models
{
    public class MatchOdds : BaseEntity
    {
        public long MatchID { get; set; }
        public Match Match { get; set; }
        public string Specifier { get; set; }
        public float Odd { get; set; }
    }
}
