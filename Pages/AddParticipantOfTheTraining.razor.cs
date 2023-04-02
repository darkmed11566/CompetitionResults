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
    public partial class AddParticipantOfTheTraining
    {

        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        protected IEnumerable<ParticipantOfTheTraining> participantOfTheTraining = new List<ParticipantOfTheTraining>();
        protected IEnumerable<Sportsman> SportsmansForSelect = new List<Sportsman>();
        protected IEnumerable<Training> TrainingForSelect = new List<Training>();
        protected ParticipantOfTheTraining participantOfTheTrainingModel = new ParticipantOfTheTraining { IsActive = true, };
        protected IEnumerable<BoatClasses> BoatClassesForSelect = new List<BoatClasses>();
        protected IEnumerable<StatusSportsmanInTrack> StatusForSelect = new List<StatusSportsmanInTrack>();

        protected override async Task OnInitializedAsync()
        {

            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            SportsmansForSelect = await context.Sportsmens
                .AsNoTracking()
                .Where(x => x.IsActive)
                .ToListAsync();

            participantOfTheTraining = await context.ParticipantOfTheTrainings
                .AsNoTracking()
                .ToListAsync();

            TrainingForSelect = await context.Trainings
                .AsNoTracking()
                .Where(x => x.IsActive).ToListAsync();

            participantOfTheTrainingModel.SportsmanId = SportsmansForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            participantOfTheTrainingModel.TrainingId = TrainingForSelect
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

            participantOfTheTraining = await context.ParticipantOfTheTrainings
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }


        private async Task SaveParticipantOfTheTraining()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (participantOfTheTrainingModel.Id is 0)
            {
                await context.ParticipantOfTheTrainings.AddAsync(participantOfTheTrainingModel);
            }
            else
            {
                context.ParticipantOfTheTrainings.Update(participantOfTheTrainingModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private void EditParticipantOfTheTraining(ParticipantOfTheTraining participantOfTheTrainingToEdit)
        {
            var shallowCopy = new ParticipantOfTheTraining
            {
                Id = participantOfTheTrainingToEdit.Id,
                Name = participantOfTheTrainingToEdit.Name,
                TrainingId = participantOfTheTrainingToEdit.TrainingId,
                BoatClass = participantOfTheTrainingToEdit.BoatClass,
                IsActive = participantOfTheTrainingToEdit.IsActive,
                StatusInTrack = participantOfTheTrainingToEdit.StatusInTrack,
                SportsmanId = participantOfTheTrainingToEdit.SportsmanId
            };

            participantOfTheTrainingModel = shallowCopy;
        }

        private async Task DeleteParticipantOfTheTrainingAsync(ParticipantOfTheTraining participantOfTheTrainingToDelete)
        {
            participantOfTheTrainingToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.ParticipantOfTheTrainings.Update(participantOfTheTrainingToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            participantOfTheTrainingModel = new ParticipantOfTheTraining
            {
                IsActive = true,
                BoatClass = BoatClasses.K1M,
                Name = string.Empty,
                StatusInTrack = StatusSportsmanInTrack.OnTraining,
                TrainingId = TrainingForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0,
                SportsmanId = SportsmansForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }

    }
}
