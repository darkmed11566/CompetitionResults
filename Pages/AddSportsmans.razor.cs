using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddSportsmans
    {
        private IEnumerable<Sportsman> sportsman = new List<Sportsman>();

        protected override void OnInitialized()
        {
            sportsman = sportsmanRepository.GetAll();
        }

        private string newName;
        private string newSecondName;
        private DateTime newDateOfBirt;
        private Sex newSex;
        private ListOfCountries newCountry;
        private Rangs newRang;
        private string newNameOfClub;
        private string newNamesOfCoachers;

        private void AddSportsman()
        {
            if (newNameOfClub == null)
            {
                newNameOfClub = "Personally";
            }

            var dbSportsman = new Sportsman();

            dbSportsman.Name = newName;
            dbSportsman.SecondName = newSecondName;
            dbSportsman.Sex = newSex;
            dbSportsman.DateOfBirth = newDateOfBirt;
            dbSportsman.Country = newCountry;
            dbSportsman.Rang = newRang;
            dbSportsman.NameOfClub = newNameOfClub;
            dbSportsman.NamesOfCoaches = newNamesOfCoachers;
            dbSportsman.IsActive = true;

            sportsmanRepository.Save(dbSportsman);

        }

        private void DeleteSportsman(long id)
        {
            sportsmanRepository.Remove(id);
        }
    }
}
