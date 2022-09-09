using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddJudges
    {
        private IEnumerable<Judge> judge = new List<Judge>();

        protected override void OnInitialized()
        {
            judge = judgeRepository.GetAll();
        }

        private string newName;
        private string newSecondName;
        private ListOfCountries newCountry;

        private void AddJudge()
        {

            var dbJudge = new Judge();

            dbJudge.Name = newName;
            dbJudge.SecondName = newSecondName;
            dbJudge.Country = newCountry;
            dbJudge.IsActive = true;

            judgeRepository.Save(dbJudge);

        }

        private void DeleteJudge(long id)
        {
            judgeRepository.Remove(id);
        }

    }
}
