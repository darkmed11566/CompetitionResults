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
    public partial class ViewTracks
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        protected IEnumerable<Track> tracks = new List<Track>();         
        
        [Parameter]
        public int CompetitionId { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("CompetitionId", out var competitionId))
            {
                CompetitionId = Convert.ToInt32(competitionId);
            }                     

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            tracks = await context.Tracks
                .AsNoTracking()
                .Where(x =>x.IsActive && x.CompetitionId==CompetitionId)
                .ToListAsync();           
        }
       
    }
}