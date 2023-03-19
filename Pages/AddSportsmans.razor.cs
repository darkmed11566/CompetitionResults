using CompetitionResults.Data;
using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddSportsmans
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Sportsman> sportsmans = new List<Sportsman>();

        protected IEnumerable<ListOfCountries> CountryForSelect = new List<ListOfCountries>();
        protected IEnumerable<Sex> SexForSelect = new List<Sex>();
        protected IEnumerable<Rangs> RangsForSelect = new List<Rangs>();
        protected IEnumerable<Coach> CoachForSelect = new List<Coach>();

        protected Sportsman sportsmanModel = new Sportsman { IsActive = true };
        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            CountryForSelect = Enum.GetValues(typeof(ListOfCountries))
               .OfType<ListOfCountries>()
               .ToList();

            SexForSelect = Enum.GetValues(typeof(Sex))
                .OfType<Sex>()
                .ToList();

            RangsForSelect = Enum.GetValues(typeof(Rangs))
                 .OfType<Rangs>()
                 .ToList();

            CoachForSelect= await context.Coaches
               .AsNoTracking()
               .Where(x => x.IsActive)
               .ToListAsync();

            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            sportsmans = await context.Sportsmens
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }       

        private void EditSportsman(Sportsman sportsmanToEdit)
        {
            var shallowCopy = new Sportsman
            {
                Id = sportsmanToEdit.Id,
                IsActive = sportsmanToEdit.IsActive,
                Name = sportsmanToEdit.Name,
                SecondName = sportsmanToEdit.SecondName,
                DateOfBirth = sportsmanToEdit.DateOfBirth,
                Sex = sportsmanToEdit.Sex,
                Country = sportsmanToEdit.Country,
                Rang = sportsmanToEdit.Rang,
                ClubName = sportsmanToEdit.ClubName,
                CoachId = sportsmanToEdit.CoachId,
                Rating = sportsmanToEdit.Rating,
                Achievements = sportsmanToEdit.Achievements,
                URLPhoto = sportsmanToEdit.URLPhoto
            };

            sportsmanModel = shallowCopy;
        }




        private async Task DeleteSportsmanAsync(Sportsman sportsmanToDelete)
        {
            sportsmanModel.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Sportsmens.Update(sportsmanToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }


        private async Task SaveSportsman()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (sportsmanModel.Id is 0)
            {

                if (sportsmanModel.ClubName == null)
                {
                    sportsmanModel.ClubName = "Personally";
                }

                await context.Sportsmens.AddAsync(sportsmanModel);
            }
            else
            {
                context.Sportsmens.Update(sportsmanModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            sportsmanModel = new Sportsman
            {
                IsActive = true,
                Name = "Enter name",
                SecondName = "Enter secondname",
                Rang = Rangs.NR,
                Country = ListOfCountries.BLR,
                Rating = 0,
                Sex = Sex.Male,
                URLPhoto = "Enter photo url",
                ClubName = "Personally",
                CoachId = CoachForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0,
                DateOfBirth = new DateTime (1950,01,01),
                Achievements = "Enter  achievements"
                
            };

            await RenewStateAsync();
        }
    }
}
