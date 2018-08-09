using System;
using System.Collections.Generic;

namespace SWSC.Entities
{
    public class StarshipResults
    {
        public string previous { get; set; }

        public string next { get; set; }

        public string previousPageNo { get; set; }

        public string nextPageNo { get; set; }

        public Int64 count { get; set; }

        public List<Starship> results { get; set; }
    }
}
