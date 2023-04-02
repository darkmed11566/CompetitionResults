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
    public partial class AddTrainingGate
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithTimeTraining> gateWithTime = new List<GateWithTimeTraining>();
        protected IEnumerable<GateWithPenaltyTraining> gateWithPenalty = new List<GateWithPenaltyTraining>();

        protected GateWithPenaltyTraining gateWithPenaltyModel = new GateWithPenaltyTraining();
        protected GateWithTimeTraining gateWithTimeModel = new GateWithTimeTraining();

        protected IEnumerable<Training> TrainingForSelect = new List<Training>();

        protected IEnumerable<GateType> GateTypesForSelect = new List<GateType>();
        protected IEnumerable<GateNameWithPenalty> GateNamesWithPenaltyForSelect = new List<GateNameWithPenalty>();
        protected IEnumerable<GateNameWithTime> GateNamesWithTimeForSelect = new List<GateNameWithTime>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            TrainingForSelect = await context.Trainings
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();

            await ResetDataToDefaultGateWithPenaltyAsync();           

            GateTypesForSelect = Enum.GetValues(typeof(GateType))
               .OfType<GateType>()
               .ToList();

            GateNamesWithPenaltyForSelect = Enum.GetValues(typeof(GateNameWithPenalty))
               .OfType<GateNameWithPenalty>()
               .ToList();

            GateNamesWithTimeForSelect = Enum.GetValues(typeof(GateNameWithTime))
              .OfType<GateNameWithTime>()
              .ToList();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTime = await context.GateWithTimeTrainings
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();

            gateWithPenalty = await context.GateWithPenaltyTrainings
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();

        }

        private void EditGateWithPenalty(GateWithPenaltyTraining gateWithPenaltyToEdit)
        {
            var shallowCopy = new GateWithPenaltyTraining
            {
                Id = gateWithPenaltyToEdit.Id,
                IsActive = gateWithPenaltyToEdit.IsActive,
                GateNumber = gateWithPenaltyToEdit.GateNumber,
                TrainingId = gateWithPenaltyToEdit.TrainingId,
                Type = gateWithPenaltyToEdit.Type

            };

            gateWithPenaltyModel = shallowCopy;
        }

        private async Task DeleteGateWithPenaltyAsync(GateWithPenaltyTraining gateWithPenaltyToDelete)
        {
            gateWithPenaltyToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithPenaltyTrainings.Update(gateWithPenaltyToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private void EditGateWithTime(GateWithTimeTraining gateWithTimeToEdit)
        {
            var shallowCopy = new GateWithTimeTraining
            {
                Id = gateWithTimeToEdit.Id,
                IsActive = gateWithTimeToEdit.IsActive,
                GateName = gateWithTimeToEdit.GateName,
                TrainingId = gateWithTimeToEdit.TrainingId,
                Type = gateWithTimeToEdit.Type
            };

            gateWithTimeModel = shallowCopy;
        }

        private async Task DeleteGateWithTimeAsync(GateWithTimeTraining gateWithTimeToDelete)
        {
            gateWithTimeToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithTimeTrainings.Update(gateWithTimeToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task SaveGateWithTime()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimeModel.Id is 0)
            {

                await context.GateWithTimeTrainings.AddAsync(gateWithTimeModel);
            }
            else
            {
                context.GateWithTimeTrainings.Update(gateWithTimeModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();

            await ResetDataToDefaultGateWithTimeAsync();
        }

        private async Task SaveGateWithPenalty()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithPenaltyModel.Id is 0)
            {

                await context.GateWithPenaltyTrainings.AddAsync(gateWithPenaltyModel);
            }
            else
            {
                context.GateWithPenaltyTrainings.Update(gateWithPenaltyModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultGateWithPenaltyAsync();

        }

        private async Task ResetDataToDefaultGateWithPenaltyAsync()

        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyModel = new GateWithPenaltyTraining
            {
                IsActive = true,
                Type = GateType.StraightGate,
                GateNumber = GateNameWithPenalty.Gate1,
                TrainingId = TrainingForSelect
                  .OrderBy(x => x.Id)
                  .FirstOrDefault()
                  ?.Id ?? 0
            };

            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultGateWithTimeAsync()

        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimeModel = new GateWithTimeTraining
            {
                IsActive = true,
                Type = GateType.TimeGate,
                GateName = GateNameWithTime.Point1,
                TrainingId = TrainingForSelect
                    .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }

    }
}
