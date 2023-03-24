using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithPenaltyTraining : Gate
    {
        public GateNameWithPenalty GateNumber { get; set; }
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }       
        public virtual List<GateWithPenaltyPassageTraining> PenaltyPassages { get; set; }
    }
}
