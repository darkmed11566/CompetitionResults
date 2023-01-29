using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithTime:Gate
    {
        public int TrackId { get; set; }
        public GateNameWithTime GateName { get; set; }
        public virtual Track Track { get; set; }
        public virtual List<GateWithTimePassage> TimePassages { get; set; }
    }
}
