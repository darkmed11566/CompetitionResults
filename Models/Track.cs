using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Track : BaseModel
    {
        public TrackType  TrackType { get; set; }
        public int CompetitionId { get; set; }
        public bool IsFull { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual List<Sector> Sectors { get; set; }
        public virtual List<GateWithTime> Gates { get; set; }
    }
}
