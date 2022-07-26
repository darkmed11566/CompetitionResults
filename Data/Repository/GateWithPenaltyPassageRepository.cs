using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class GateWithPenaltyPassageRepository : BaseRepository<GateWithPenaltyPassage>
    {
        public GateWithPenaltyPassageRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
