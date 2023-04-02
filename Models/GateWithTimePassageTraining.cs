using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithTimePassageTraining : BaseModel
    {
        public DateTime GatePasssage { get; set; }
        public int GateWithTimeTrainingId { get; set; }
        public int ParticipantOfTheTrainingId { get; set; }
        public virtual GateWithTimeTraining TimeGate { get; set; }
        public virtual ParticipantOfTheTraining ParticipantOfTheTraining { get; set; }
    }
}
