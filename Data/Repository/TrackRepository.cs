using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class SportsmanRepository : BaseRepository<Sportsman>
    {
        public SportsmanRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
