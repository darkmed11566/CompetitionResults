using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Data.Repository
{
    public class JudgeRepository : BaseRepository<Judge>
    {
        public JudgeRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
