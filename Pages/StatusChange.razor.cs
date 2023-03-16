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
    public partial class StatusChange
    {

        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        protected IEnumerable<Competitioner> CompetitionersForSelect = new List<Competitioner>();
        protected Competitioner competitionerModel = new Competitioner { IsActive = true, };
        protected IEnumerable<StatusSportsmanInTrack> StatusForSelect = new List<StatusSportsmanInTrack>();

        [Parameter]
        public int IdCompetitionFromPage { get; set; }

        protected override async Task OnInitializedAsync()
        {          
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            CompetitionersForSelect = await context.Competitioners
                .AsNoTracking()
                .Where(x => x.IsActive
                //&& x.StatusInTrack == StatusSportsmanInTrack.RegisteredForCompetition
                && x.CompetitionId == IdCompetitionFromPage)
                .ToListAsync();

            StatusForSelect = Enum.GetValues(typeof(StatusSportsmanInTrack))
            .OfType<StatusSportsmanInTrack>()
            .ToList();            
        }  
        private async Task UpdateStatus()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (competitionerModel.Id is 0)
            {
                await context.Competitioners.AddAsync(competitionerModel);
            }
            else
            {
                context.Competitioners.Update(competitionerModel);
            }
            await context.SaveChangesAsync();           
        }
        private void EditCompetitioner(Competitioner competitionerToEdit)
        {
            var shallowCopy = new Competitioner
            {
                Id = competitionerToEdit.Id,
                Number = competitionerToEdit.Number,
                CompetitionId = competitionerToEdit.CompetitionId,
                BoatClass = competitionerToEdit.BoatClass,
                IsActive = competitionerToEdit.IsActive,
                StatusInTrack = competitionerToEdit.StatusInTrack,
                SportsmanId = competitionerToEdit.SportsmanId
            };

            competitionerModel = shallowCopy;
        }
    }

}

