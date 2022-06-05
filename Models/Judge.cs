using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Judge:BaseModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }        
        public ListOfCountries Country { get; set; }        
    }
}
