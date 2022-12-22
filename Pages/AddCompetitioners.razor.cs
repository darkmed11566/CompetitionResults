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
    public partial class AddCompetitioners
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        private IEnumerable<Competitioner> competitioner = new List<Competitioner>();

        protected override async Task OnInitializedAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            competitioner = await context.Competitioners.AsNoTracking().ToListAsync();
            SportsmansForSelect = await context.Sportsmens.AsNoTracking()
                .Where(x=> x.IsActive == true).ToListAsync();
            newSportsman = SportsmansForSelect.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
        }

        private int newNumber;
        private BoatClasses newClass;
        private IEnumerable<Sportsman> SportsmansForSelect = new List<Sportsman>();
        private long newSportsman;
        private async Task AddCompetitioner()
        {

            var dbCompetitioner = new Competitioner();

            dbCompetitioner.Number = newNumber;
            dbCompetitioner.BoatClass = newClass;
            dbCompetitioner.IsActive = true;
            dbCompetitioner.StatusInTrack = StatusSportsmanInTrack.RegisteredForCompetition;
            dbCompetitioner.SportsmanId = newSportsman;

            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.Competitioners.AddAsync(dbCompetitioner);
            await context.SaveChangesAsync();

        }
        protected void TrackSelected(ChangeEventArgs args)
        {
            var x = args.Value;
        }

        private void DeleteCompetitioner(long id)
        {
            competitionerRepository.Remove(id);
        }
    }
}
