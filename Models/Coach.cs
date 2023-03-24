using CompetitionResults.EnumsAndConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Models
{
    public class Coach : BaseModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public virtual List<Training> Trainings { get; set; }
        public virtual List<Sportsman> Sportsmens { get; set; }
    }
}
