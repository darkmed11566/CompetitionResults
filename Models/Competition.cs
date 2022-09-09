using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Competition: BaseModel
    {
        public string CompetitionName { get; set; }
        public DateTime CompetitionStartData { get; set; }
        public DateTime CompetitionEndtData { get; set; }
        public bool Rating { get; set; }
        public CompetitionType Type { get; set; }
        public ListOfCountries Country { get; set; }
        public CompetitionStatus Status { get; set; }
        //public virtual List<Track> CompetitionTracks { get; set; }
    }
}
