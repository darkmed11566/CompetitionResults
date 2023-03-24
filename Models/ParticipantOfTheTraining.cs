using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class ParticipantOfTheTraining : BaseModel
    {
        public string Name { get; set; }
        public BoatClasses BoatClass { get; set; }
        public StatusSportsmanInTrack StatusInTrack { get; set; }
        public int SportsmanId { get; set; }
        public int TrainingId{ get; set; }
        public virtual Sportsman Sportsman { get; set; }
        public virtual Training Training { get; set; }
        public virtual List<GateWithPenaltyPassageTraining> GateWithPenaltyPassages { get; set; }
        public virtual List<GateWithTimePassageTraining> GateWithTimePassages { get; set; }
    }
}

