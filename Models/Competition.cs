using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Competition : BaseModel
    {
        public string NameOfCompetition { get; set; }
        public DateTime CompetitionStartData { get; set; }
        public DateTime CompetitionEndData { get; set; }
        public bool IsRating { get; set; }
        public bool IsOpen { get; set; }
        public ListOfCountries Country { get; set; }

    }
}
