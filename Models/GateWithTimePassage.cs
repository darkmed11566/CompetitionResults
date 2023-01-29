using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithTimePassage : BaseModel
    {
        public DateTime GatePasssage { get; set; }
        public int GateWihtTimeId { get; set; }
        public int CompetitionerId { get; set; }
        public virtual GateWithTime TimaGate { get; set; }
        public virtual Competitioner Competitioner { get; set; }
    }
}
