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
    public partial class AddCompetitioners
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        protected IEnumerable<Competitioner> competitioner = new List<Competitioner>();        
        protected IEnumerable<Sportsman> SportsmansForSelect = new List<Sportsman>();
        protected IEnumerable<Competition> CompetitionsForSelect = new List<Competition>();
        protected Competitioner competitionerModel = new Competitioner { IsActive = true,};
        protected IEnumerable<BoatClasses> BoatClassesForSelect = new List<BoatClasses>();
        protected IEnumerable<StatusSportsmanInTrack> StatusForSelect = new List<StatusSportsmanInTrack>();

        protected override async Task OnInitializedAsync()
        {

            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            SportsmansForSelect = await context.Sportsmens
                .AsNoTracking()
                .Where(x=> x.IsActive)
                .ToListAsync();

            competitioner = await context.Competitioners
                .AsNoTracking()
                .ToListAsync();

            CompetitionsForSelect = await context.Competitions
                .AsNoTracking()
                .Where(x =>x.IsActive).ToListAsync();

            competitionerModel.SportsmanId = SportsmansForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            competitionerModel.CompetitionId = CompetitionsForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            BoatClassesForSelect = Enum.GetValues(typeof(BoatClasses))
               .OfType<BoatClasses>()
               .ToList();

            StatusForSelect = Enum.GetValues(typeof(StatusSportsmanInTrack))
               .OfType<StatusSportsmanInTrack>()
               .ToList();

            await ResetDataToDefaultAsync();
        }


        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitioner = await context.Competitioners
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }


        private async Task SaveCompetitioner()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (competitionerModel.Id is 0)
            {
                await context.Competitioners.AddAsync(competitionerModel);
            }
            else
            {
                context.Competitioners.Update(competitionerModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();

            await ResetDataToDefaultAsync();
        }

        private void EditCompetitioner(Competitioner competitionerToEdit)
        {
            var shallowCopy = new Competitioner
            {
                Id = competitionerToEdit.Id,
                Number = competitionerToEdit.Number,
                CompetitionId = competitionerToEdit.CompetitionId,
                BoatClass = competitionerToEdit.BoatClass,
                IsActive = competitionerToEdit.IsActive,
                StatusInTrack =competitionerToEdit.StatusInTrack,
                SportsmanId=competitionerToEdit.SportsmanId
            };

            competitionerModel = shallowCopy;
        }

        private async Task DeleteCompetitionerAsync(Competitioner competitionerToDelete)
        {
            competitionerToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Competitioners.Update(competitionerToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitionerModel = new Competitioner
            {
                IsActive = true,
                BoatClass = BoatClasses.K1M,
                Number = 1,
                StatusInTrack = StatusSportsmanInTrack.RegisteredForCompetition,
                CompetitionId = CompetitionsForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0,
                SportsmanId= SportsmansForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0

            };

            await RenewStateAsync();
        }
    }
}
