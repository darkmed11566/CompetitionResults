using CompetitionResults.Data;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddCoach
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Coach> coach = new List<Coach>();

        protected Coach coachModel = new Coach { IsActive = true };
        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            coach = await context.Coaches
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        private void EditCoach(Coach coachToEdit)
        {
            var shallowCopy = new Coach
            {
                Id = coachToEdit.Id,
                IsActive = coachToEdit.IsActive,
                Name = coachToEdit.Name,
                SecondName = coachToEdit.SecondName
            };

            coachModel = shallowCopy;
        }
        private async Task DeleteCoachAsync(Coach coachToDelete)
        {
            coachModel.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Coaches.Update(coachToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }


        private async Task SaveCoach()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (coachModel.Id is 0)
            {

                await context.Coaches.AddAsync(coachModel);
            }
            else
            {
                context.Coaches.Update(coachModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            coachModel = new Coach
            {
                IsActive = true,
                Name = "Enter name",
                SecondName = "Enter secondname"
            };

            await RenewStateAsync();
        }
    }

}

