using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
