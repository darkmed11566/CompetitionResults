using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Sportsman:BaseModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public ListOfCountries Country { get; set; }
        public Rangs Rang { get; set; }
        public string ClubName { get; set; }
        public string URLPhoto { get; set; }
        public string Achievements { get; set; }
        public int Rating { get; set; }
        public int CoachId { get; set; }
        public virtual Coach Coach { get; set; }
        public virtual List<Competitioner> Competitioners { get; set; }
        
    }
}
