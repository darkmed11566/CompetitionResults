using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class GateWithPenaltyRepository:BaseRepository<GateWithPenalty>
    {
        public GateWithPenaltyRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
