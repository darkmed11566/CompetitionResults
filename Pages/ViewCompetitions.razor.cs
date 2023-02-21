using CompetitionResults.Data;
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
    public partial class ViewCompetitions
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        protected IEnumerable<Competition> competitions = new List<Competition>();
        protected Competition competitionModel = new Competition { IsActive = true };

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

        }
        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitions = await context.Competitions
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }
    }
}