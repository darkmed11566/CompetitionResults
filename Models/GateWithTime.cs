using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class GateWithTime:Gate
    {
        public long TrackId { get; set; }
        public GateNameWithTime GateName { get; set; }
        public virtual Track Track { get; set; }
    }
}
