using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Sector : BaseModel
    {
        public int Number { get; set; }
        public int TrackId { get; set; }
        public bool IsFull { get; set; }
        public virtual Track Track { get; set; }
        public virtual List<GateWithPenalty> GatesWithPenalty { get; set; }
    }
}
