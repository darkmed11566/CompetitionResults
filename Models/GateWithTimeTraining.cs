using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithTimeTraining : Gate
    {
        public int TrainingId { get; set; }
        public GateNameWithTime GateName { get; set; }
        public virtual Training Training { get; set; }        
        public virtual List<GateWithTimePassageTraining> TimePassages { get; set; }
    }
}
