using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class GateWithTimeRepository:BaseRepository<GateWithTime>
    {
        public  GateWithTimeRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
