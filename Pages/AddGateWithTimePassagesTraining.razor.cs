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
    public partial class AddGateWithTimePassagesTraining
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithTimePassageTraining> gateWithTimePassage = new List<GateWithTimePassageTraining>();
        protected GateWithTimePassageTraining gateWithTimePassageModel = new GateWithTimePassageTraining { IsActive = true };

        protected IEnumerable<ParticipantOfTheTraining> ParticipantOfTheTrainingForSelect = new List<ParticipantOfTheTraining>();
        protected IEnumerable<GateWithTimeTraining> GateWithTimeForSelect = new List<GateWithTimeTraining>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            ParticipantOfTheTrainingForSelect = await context.ParticipantOfTheTrainings
               .AsNoTracking()
               .Where(x => x.IsActive && x.StatusInTrack == StatusSportsmanInTrack.OnTraining)
               .ToListAsync();

            GateWithTimeForSelect = await context.GateWithTimeTrainings
              .Where(x => x.IsActive)
              .ToListAsync();

            gateWithTimePassageModel.GateWithTimeTrainingId = GateWithTimeForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithTimePassageModel.ParticipantOfTheTrainingId = ParticipantOfTheTrainingForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            await ResetDataToDefaultAsync();
        }

        private void EditGateWithTimePasseage(GateWithTimePassageTraining passageToEdit)
        {
            var shallowCopy = new GateWithTimePassageTraining
            {
                Id = passageToEdit.Id,
                GatePasssage = passageToEdit.GatePasssage,
                ParticipantOfTheTrainingId = passageToEdit.ParticipantOfTheTrainingId,
                GateWithTimeTrainingId = passageToEdit.GateWithTimeTrainingId,
                IsActive = passageToEdit.IsActive
            };

            gateWithTimePassageModel = shallowCopy;
        }

        private async Task DeleteGateWithTimePasseageAsync(GateWithTimePassageTraining passageToDelete)
        {
            passageToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithTimePassageTrainings.Update(passageToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task SaveGateWithTimePasseage()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimePassageModel.Id is 0)
            {

                await context.GateWithTimePassageTrainings.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassageTrainings.Update(gateWithTimePassageModel);
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
                await context.GateWithTimePassageTrainings.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassageTrainings.Update(gateWithTimePassageModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimePassage = await context.GateWithTimePassageTrainings
           .AsNoTracking()
           .Where(t => t.IsActive)
           .ToListAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimePassageModel = new GateWithTimePassageTraining
            {
                IsActive = true,

                GatePasssage = DateTime.Now,

                GateWithTimeTrainingId = GateWithTimeForSelect
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
