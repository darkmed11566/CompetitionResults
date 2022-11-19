using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddCompetitioners
    {
        private IEnumerable<Competitioner> competitioner = new List<Competitioner>();

        protected override void OnInitialized()
        {
            competitioner = competitionerRepository.GetAll();

        }

        private int newNumber;
        private BoatClasses newClass;

        private void AddCompetitioner()
        {

            var dbCompetitioner = new Competitioner();

            dbCompetitioner.Number = newNumber;
            dbCompetitioner.Class = newClass;
            dbCompetitioner.IsActive = true;
            dbCompetitioner.StatusInTrack = StatusSportsmanInTrack.RegisteredForCompetition;

            competitionerRepository.Save(dbCompetitioner);

        }

        private void DeleteCompetitioner(long id)
        {
            competitionerRepository.Remove(id);
        }
    }
}
