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
    public partial class AddTrainings
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Training> trainings = new List<Training>();
        protected Training trainingModel = new Training { IsActive = true };
        protected IEnumerable<Coach> CoachForSelect = new List<Coach>();
        protected IEnumerable<TrainingType> TypesForSelect = new List<TrainingType>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            TypesForSelect = Enum.GetValues(typeof(TrainingType))
               .OfType<TrainingType>()
               .ToList();

            CoachForSelect = await context.Coaches
              .AsNoTracking()
              .Where(x => x.IsActive)
              .ToListAsync();

            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            trainings = await context.Trainings
                .AsNoTracking()
                .Include(x=>x.Coach)
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        private async Task SaveTraining()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (trainingModel.Id is 0)
            {

                await context.Trainings.AddAsync(trainingModel);
            }
            else
            {
                context.Trainings.Update(trainingModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();

        }

        private void EditTraining(Training trainingToEdite)
        {
            var shallowCopy = new Training
            {
                Id = trainingToEdite.Id,
                CoachId = trainingToEdite.CoachId,
                IsActive = trainingToEdite.IsActive,
                Type = trainingToEdite.Type,
                TrainingDate = trainingToEdite.TrainingDate
            };

            trainingModel = shallowCopy;
        }

        private async Task DeleteTrainingAsync(Training trainingToDelete)
        {
            trainingToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Trainings.Update(trainingToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            trainingModel = new Training
            {
                IsActive = true,
                Type = TrainingType.ShortTrack,
                TrainingDate = DateTime.Now,
                CoachId = CoachForSelect
                 .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }
    }
}
