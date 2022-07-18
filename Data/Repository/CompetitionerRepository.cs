using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class CompetitionerRepository : BaseRepository<Competitioner>
    {
        public CompetitionerRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
