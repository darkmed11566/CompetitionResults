using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithPenalty:Gate
    {
        public GateNameWithPenalty GateNumber { get; set; }
        public int SectorId { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual List<GateWithPenaltyPassage> PenaltyPassages { get; set; }
    }
}
