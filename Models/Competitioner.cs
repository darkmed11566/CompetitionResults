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
        public BoatClasses Class { get; set; }
        public StatusSportsmanInTrack StatusInTrack { get; set; }
    }
}
