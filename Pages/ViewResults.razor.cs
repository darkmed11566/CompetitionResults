using CompetitionResults.Data;
using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class ViewResults
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }       
        protected IEnumerable<Competitioner> competitioner = new List<Competitioner>();
        protected IEnumerable<Competitioner> KM1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> KW1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> CM1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> CW1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MW = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MM = new List<Competitioner>();
        protected IEnumerable<Competitioner> K1M3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> K1W3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C1M3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C1W3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MW3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MM3 = new List<Competitioner>();
        
        protected int TotalPenalty { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected IEnumerable<Track> tracks = new List<Track>();

        [Parameter]
        public int TrackId { get; set; }
        [Parameter]
        public int CompetitionId { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("TrackId", out var trackId))
            {
                TrackId = Convert.ToInt32(trackId);
            }

            var uriC = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uriC.Query).TryGetValue("CompetitionId", out var competitionId))
            {
                CompetitionId = Convert.ToInt32(competitionId);
            }            

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            tracks = await context.Tracks
                .AsNoTracking()
                .Where(x => x.Id == TrackId)
                .ToListAsync();

            competitioner = await context.Competitioners
                .AsNoTracking()
                .Where(x => x.CompetitionId==CompetitionId)
                .ToListAsync();

            KM1 = await context.Competitioners
                 .AsNoTracking()
                 .Where(x => x.IsActive && x.BoatClass == BoatClasses.K1M)
                 .ToListAsync();
            KW1 = await context.Competitioners
                   .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.K1M)
                   .ToListAsync();
            CM1 = await context.Competitioners
                   .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.C1M)
                   .ToListAsync();
            CW1 =  await context.Competitioners
                   .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.C1W)
                   .ToListAsync();
            C2MW = await context.Competitioners
                  .AsNoTracking()
                  .Where(x => x.IsActive && x.BoatClass == BoatClasses.C2MW)
                  .ToListAsync();
            C2MM = await context.Competitioners
                  .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.C2MM)
                   .ToListAsync();
            K1M3 = await context.Competitioners
                  .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.K1M3)
                   .ToListAsync();
            K1W3 = await context.Competitioners
                  .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.K1W3)
                   .ToListAsync();
            C1M3 = await context.Competitioners
                  .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.C1M3)
                   .ToListAsync();
            C1W3 = await context.Competitioners
                  .AsNoTracking()
                   .Where(x => x.IsActive && x.BoatClass == BoatClasses.C1W3)
                   .ToListAsync();
            C2MW3 = await context.Competitioners
                   .AsNoTracking()
                    .Where(x => x.IsActive && x.BoatClass == BoatClasses.C2MW3)
                    .ToListAsync();
            C2MM3 = await context.Competitioners
                   .AsNoTracking()
                    .Where(x => x.IsActive && x.BoatClass == BoatClasses.C2MM3)
                    .ToListAsync();
        }






        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            competitioner = await context.Competitioners
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }

    }

}

