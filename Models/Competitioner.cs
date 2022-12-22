using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Competitioner:BaseModel
    {
        public int Number { get; set; }
        public BoatClasses BoatClass { get; set; }
        public StatusSportsmanInTrack StatusInTrack { get; set; }
        public long SportsmanId { get; set; }
        public long CompetitionId { get; set; }
        public virtual Sportsman Sportsman { get; set; }
        public virtual Competition Competition { get; set; }
    }
}
