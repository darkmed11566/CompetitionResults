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
    public partial class ViewGateWithTime
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected IEnumerable<GateWithTime> gateWithTimes = new List<GateWithTime>();

        [Parameter]
        public int TrackId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("TrackId", out var trackId))
            {
                TrackId = Convert.ToInt32(trackId);
            }            

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimes = await context
                .GateWithTimes.AsNoTracking()
                .Where(x => x.IsActive && x.TrackId == TrackId)
                .ToListAsync();
        }       


    }
}
