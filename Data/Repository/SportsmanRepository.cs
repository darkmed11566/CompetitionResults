using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class TrackRepository : BaseRepository<Track>
    {
        public TrackRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
