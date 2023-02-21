using CompetitionResults.Data;
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
    public partial class ViewSectors
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected IEnumerable<Sector> sectors = new List<Sector>();

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

            sectors = await context
                .Sectors.AsNoTracking()
                .Where(x => x.IsActive && x.TrackId == TrackId)
                .ToListAsync();
        }

    }
}
