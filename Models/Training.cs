﻿using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Training : BaseModel
    {
        public TrainingType Type { get; set; }
        public int CoachId { get; set; }
        public DateTime TrainingDate { get; set; }
        public virtual Coach Coach { get; set; }
        public virtual List<ParticipantOfTheTraining> ParticipantOfTheTrainings { get; set; }
        public virtual List<GateWithPenaltyTraining> GateWithPenalty { get; set; }
        public virtual List<GateWithTimeTraining> GateWithTimes { get; set; }
    }
}
