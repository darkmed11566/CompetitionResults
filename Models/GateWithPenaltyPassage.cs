using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithPenaltyPassage : BaseModel
    {
        public int GateWihtPenaltyId { get; set; }
        public Penalties PenaltyOnGate { get; set; }
        public int CompetitionerId { get; set; }
        public virtual GateWithPenalty PenaltyGate { get; set; }
        public virtual Competitioner Competitioner { get; set; }
    }
}
