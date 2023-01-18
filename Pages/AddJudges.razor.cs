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
    public partial class AddJudges
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Judge> judges = new List<Judge>();

        protected Judge  judgeModel = new Judge { IsActive = true };

        protected IEnumerable<ListOfCountries> CountryForSelect = new List<ListOfCountries>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();            

            CountryForSelect = Enum.GetValues(typeof(ListOfCountries))
                .OfType<ListOfCountries>()
                .ToList();

            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            judges = await context.Judges
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }
               

        private async Task SaveJudge()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (judgeModel.Id is 0)
            {               
               
                await context.Judges.AddAsync(judgeModel);
            }
            else
            {
                context.Judges.Update(judgeModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();

        }

        private void EditJudge(Judge judgeToEdite)
        {
            var shallowCopy = new Judge
            {
                Id = judgeToEdite.Id,
                Country = judgeToEdite.Country,
                Name = judgeToEdite.SecondName,
                SecondName = judgeToEdite.Name,
                IsActive = judgeToEdite.IsActive
            };

            judgeModel = shallowCopy;
        }

        private async Task DeleteJudgeAsync(Judge judgeToDelete)
        {
            judgeModel.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Judges.Update(judgeToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            judgeModel = new Judge
            {               
                IsActive = true,
               Country = ListOfCountries.BLR,
                Name = "Enter name",
                SecondName = "Enter secondname"
            };

            await RenewStateAsync();
        }

    }
}
