using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithPenaltyPassageTraining : BaseModel
    {
        public int GateWihtPenaltyTrainingId { get; set; }
        public Penalties PenaltyOnGate { get; set; }
        public int ParticipantOfTheTrainingId { get; set; }
        public virtual GateWithPenaltyTraining GateWithPenalty { get; set; }
        public virtual ParticipantOfTheTraining ParticipantOfTheTraining { get; set; }
    }
}
