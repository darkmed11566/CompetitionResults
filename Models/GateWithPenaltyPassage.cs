using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithPenaltyPassage : BaseModel
    {
        public Penalties PenaltyOnGate { get; set; }
    }
}
