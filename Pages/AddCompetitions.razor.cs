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
    public partial class AddCompetitions
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Competition> competitions = new List<Competition>();

        protected IEnumerable<ListOfCountries> CountryForSelect = new List<ListOfCountries>();
        protected IEnumerable<CompetitionType> TypesForSelect = new List<CompetitionType>();
        protected IEnumerable<CompetitionStatus> StatusForSelect = new List<CompetitionStatus>();

        protected Competition competitionModel = new Competition { IsActive = true };

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();


            CountryForSelect = Enum.GetValues(typeof(ListOfCountries))
                .OfType<ListOfCountries>()
                .ToList();

            TypesForSelect = Enum.GetValues(typeof(CompetitionType))
                .OfType<CompetitionType>()
                .ToList();

            StatusForSelect = Enum.GetValues(typeof(CompetitionStatus))
               .OfType<CompetitionStatus>()
               .ToList();

            await ResetDataToDefaultAsync();
        }
        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitions = await context.Competitions
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        private async Task SaveCompetition()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (competitionModel.Id is 0)
            {
                
                await context.Competitions.AddAsync(competitionModel);
            }
            else
            {
                context.Competitions.Update(competitionModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();

            await ResetDataToDefaultAsync();
        }

        private void EditCompetition(Competition competitionToEdit)
        {
            var shallowCopy = new Competition
            {
                Id = competitionToEdit.Id,
                IsFull = competitionToEdit.IsFull,                
                Type = competitionToEdit.Type,
                IsActive = competitionToEdit.IsActive,
                Country = competitionToEdit.Country,
                StartDate = competitionToEdit.StartDate,
                EndtDate = competitionToEdit.EndtDate,
                IsRated = competitionToEdit.IsRated,
                Name = competitionToEdit.Name,
                Status = competitionModel.Status
            };

            competitionModel = shallowCopy;
        }

        private async Task DeleteCompetitionAsync(Competition competitionToDelete)
        {
            competitionToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Competitions.Update(competitionToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitionModel = new Competition
            {
                IsActive = true,                
                 IsFull = false,
                 Type = CompetitionType.OpenTeamPersonal,
                 Country =ListOfCountries.BLR,
                 StartDate = DateTime.Now,
                 EndtDate = DateTime.Now,
                 IsRated = false,
                 Name = "Enter name",
                Status = CompetitionStatus.Planned
                
            };

            await RenewStateAsync();
        }
    }
}
