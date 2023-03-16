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
    public partial class AddGateWithPenaltyPasseges
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithPenaltyPassage> gateWithPenaltyPassage = new List<GateWithPenaltyPassage>();
        protected GateWithPenaltyPassage gateWithPenaltyPassageModel = new GateWithPenaltyPassage { IsActive = true };

        protected IEnumerable<Competitioner> CompetitionersForSelect = new List<Competitioner>();
        protected IEnumerable<GateWithPenalty> GateWithPenaltiesForSelect = new List<GateWithPenalty>();
        protected IEnumerable<Penalties> PenaltiesForSelect = new List<Penalties>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            CompetitionersForSelect = await context.Competitioners
               .AsNoTracking()
               .Where(x => x.IsActive /*&& x.StatusInTrack==StatusSportsmanInTrack.OnTrack*/)
               .ToListAsync();

            GateWithPenaltiesForSelect = await context.GateWithPenalties
              .AsNoTracking()
              .Where(x => x.IsActive)
              .ToListAsync();

            gateWithPenaltyPassageModel.GateWihtPenaltyId = GateWithPenaltiesForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithPenaltyPassageModel.CompetitionerId = CompetitionersForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            PenaltiesForSelect = Enum.GetValues(typeof(Penalties))
               .OfType<Penalties>()
               .ToList();

            await ResetDataToDefaultAsync();
        }

        private async Task SaveGateWithPenaltyPasseage()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithPenaltyPassageModel.Id is 0)
            {

                await context.GateWithPenaltiePassages.AddAsync(gateWithPenaltyPassageModel);
            }
            else
            {
                context.GateWithPenaltiePassages.Update(gateWithPenaltyPassageModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private void EditGateWithPenaltyPasseage(GateWithPenaltyPassage passageToEdit)
        {
            var shallowCopy = new GateWithPenaltyPassage
            {
                Id = passageToEdit.Id,
                CompetitionerId = passageToEdit.CompetitionerId,
                PenaltyOnGate = passageToEdit.PenaltyOnGate,
                GateWihtPenaltyId = passageToEdit.GateWihtPenaltyId,
                IsActive = passageToEdit.IsActive
            };

            gateWithPenaltyPassageModel = shallowCopy;
        }

        private async Task DeleteGateWithPenaltyPasseageAsync(GateWithPenaltyPassage passageToDelete)
        {
            passageToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithPenaltiePassages.Update(passageToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }
        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyPassage = await context.GateWithPenaltiePassages
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }
        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyPassageModel = new GateWithPenaltyPassage
            {
                IsActive = true,

                PenaltyOnGate = Penalties.CleanPassage,

                GateWihtPenaltyId = GateWithPenaltiesForSelect
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
