using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class CompetitionRepository : BaseRepository<Competition>
    {
        public CompetitionRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
