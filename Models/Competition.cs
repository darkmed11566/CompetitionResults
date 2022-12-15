using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Competition: BaseModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public bool IsRated { get; set; }
        public bool IsFull { get; set; }
        public CompetitionType Type { get; set; }
        public ListOfCountries Country { get; set; }
        public CompetitionStatus Status { get; set; }
        public virtual List<Track> Tracks { get; set; }
    }
}
