using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddCompetitions
    {

        private IEnumerable<Competition> competition = new List<Competition>();

        protected override void OnInitialized()
        {
            competition = competitionRepository.GetAll();
        }

        private string newCompetitionName;
        private DateTime newCompetitionStartData;
        private DateTime newCompetitionEndData;
        private bool newRating;
        private ListOfCountries newCountry;
        private CompetitionType newType;

        private void AddCompetition()
        {

            var dbCompetition = new Competition();

            dbCompetition.Name = newCompetitionName;
            dbCompetition.StartDate = newCompetitionStartData;
            dbCompetition.IsActive = true;
            dbCompetition.EndtDate = newCompetitionEndData;
            dbCompetition.Country = newCountry;
            dbCompetition.IsRated = newRating;
            dbCompetition.Type = newType;
            dbCompetition.Status = CompetitionStatus.Planned;

            competitionRepository.Save(dbCompetition);
        }

        private void DeleteCompetition(long id)
        {
            competitionRepository.Remove(id);
        }
    }
}
