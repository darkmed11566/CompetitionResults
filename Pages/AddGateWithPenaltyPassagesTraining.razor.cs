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
    public partial class AddGateWithPenaltyPassagesTraining
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithPenaltyPassageTraining> gateWithPenaltyPassage = new List<GateWithPenaltyPassageTraining>();
        protected GateWithPenaltyPassageTraining gateWithPenaltyPassageModel = new GateWithPenaltyPassageTraining { IsActive = true };

        protected IEnumerable<ParticipantOfTheTraining> ParticipantOfTheTrainingForSelect = new List<ParticipantOfTheTraining>();
        protected IEnumerable<GateWithPenaltyTraining> GateWithPenaltiesForSelect = new List<GateWithPenaltyTraining>();
        protected IEnumerable<Penalties> PenaltiesForSelect = new List<Penalties>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            ParticipantOfTheTrainingForSelect = await context.ParticipantOfTheTrainings
               .AsNoTracking()
               .Where(x => x.IsActive && x.StatusInTrack == StatusSportsmanInTrack.OnTraining)
               .ToListAsync();

            GateWithPenaltiesForSelect = await context.GateWithPenaltyTrainings
              .AsNoTracking()
              .Where(x => x.IsActive)
              .ToListAsync();

            gateWithPenaltyPassageModel.GateWihtPenaltyTrainingId = GateWithPenaltiesForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithPenaltyPassageModel.ParticipantOfTheTrainingId = ParticipantOfTheTrainingForSelect
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

                await context.GateWithPenaltyPassageTrainings.AddAsync(gateWithPenaltyPassageModel);
            }
            else
            {
                context.GateWithPenaltyPassageTrainings.Update(gateWithPenaltyPassageModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private void EditGateWithPenaltyPasseage(GateWithPenaltyPassageTraining passageToEdit)
        {
            var shallowCopy = new GateWithPenaltyPassageTraining
            {
                Id = passageToEdit.Id,
                ParticipantOfTheTrainingId = passageToEdit.ParticipantOfTheTrainingId,
                PenaltyOnGate = passageToEdit.PenaltyOnGate,
                GateWihtPenaltyTrainingId = passageToEdit.GateWihtPenaltyTrainingId,
                IsActive = passageToEdit.IsActive
            };

            gateWithPenaltyPassageModel = shallowCopy;
        }

        private async Task DeleteGateWithPenaltyPasseageAsync(GateWithPenaltyPassageTraining passageToDelete)
        {
            passageToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithPenaltyPassageTrainings.Update(passageToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }
        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyPassage = await context.GateWithPenaltyPassageTrainings
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }
        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyPassageModel = new GateWithPenaltyPassageTraining
            {
                IsActive = true,

                PenaltyOnGate = Penalties.CleanPassage,

                GateWihtPenaltyTrainingId = GateWithPenaltiesForSelect
                  .OrderBy(x => x.Id)
                     .FirstOrDefault()
                     ?.Id ?? 0,

                ParticipantOfTheTrainingId = ParticipantOfTheTrainingForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }
    }
}
