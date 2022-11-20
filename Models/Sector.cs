using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Sector : BaseModel
    {
        public int Number { get; set; }
        public long TrackId { get; set; }
        public virtual Track Track { get; set; }
    }
}
