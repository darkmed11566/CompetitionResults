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
    public partial class AddGateWichTimePasseges
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithTimePassage> gateWithTimePassage = new List<GateWithTimePassage>();
        protected GateWithTimePassage gateWithTimePassageModel = new GateWithTimePassage { IsActive = true };

        protected IEnumerable<Competitioner> CompetitionersForSelect = new List<Competitioner>();
        protected IEnumerable<GateWithTime> GateWithTimeForSelect = new List<GateWithTime>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            CompetitionersForSelect = await context.Competitioners
               .AsNoTracking()
               .Where(x => x.IsActive && x.StatusInTrack == StatusSportsmanInTrack.RegisteredForCompetition)
               .ToListAsync();

            GateWithTimeForSelect = await context.GateWithTimes
              .Where(x => x.IsActive)
              .ToListAsync();

            gateWithTimePassageModel.GateWihtTimeId = GateWithTimeForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithTimePassageModel.CompetitionerId = CompetitionersForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            await ResetDataToDefaultAsync();
        }

        private void EditGateWithTimePasseage(GateWithTimePassage passageToEdit)
        {
            var shallowCopy = new GateWithTimePassage
            {
                Id = passageToEdit.Id,
                GatePasssage = passageToEdit.GatePasssage,
                CompetitionerId = passageToEdit.CompetitionerId,
                GateWihtTimeId = passageToEdit.GateWihtTimeId,
                IsActive=passageToEdit.IsActive
            };

            gateWithTimePassageModel = shallowCopy;
        }

        private async Task DeleteGateWithTimePasseageAsync(GateWithTimePassage passageToDelete)
        {
            passageToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithTimePassages.Update(passageToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task SaveGateWithTimePasseage()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimePassageModel.Id is 0)
            {
                
                await context.GateWithTimePassages.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassages.Update(gateWithTimePassageModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task SaveGateWithTimePasseageNow()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimePassageModel.Id is 0)
            {
                gateWithTimePassageModel.GatePasssage = DateTime.Now;
                await context.GateWithTimePassages.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassages.Update(gateWithTimePassageModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimePassage = await context.GateWithTimePassages
           .AsNoTracking()
           .Where(t => t.IsActive)
           .ToListAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimePassageModel = new GateWithTimePassage
            {
                IsActive = true,

                GatePasssage = DateTime.Now,
                
                GateWihtTimeId = GateWithTimeForSelect
                 .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0,

                CompetitionerId = CompetitionersForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }
    }
}
