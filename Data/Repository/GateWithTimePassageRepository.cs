using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class GateWithTimePassageRepository:BaseRepository<GateWithTimePassage>
    {
        public  GateWithTimePassageRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
