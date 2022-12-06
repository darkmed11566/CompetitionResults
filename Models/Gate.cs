using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Gate:BaseModel
    {
        public GateType Type { get; set; }
       
    }
}
