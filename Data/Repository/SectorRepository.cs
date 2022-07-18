using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class SectorRepository : BaseRepository<Sector>
    {
        public SectorRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
