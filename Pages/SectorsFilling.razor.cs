using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class SectorsFilling
    {
        [Inject]
        protected NavigationManager NavManager { get; set; }

        [Parameter]
        public int SectorId { get; set; }
        public int CompetitionId { get; set; }
        protected override async Task OnInitializedAsync()
        {            
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("SectorId", out var sectorId))
            {
                SectorId = Convert.ToInt32(sectorId);
            }

            var uriC = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uriC.Query).TryGetValue("CompetitionId", out var competitionId))
            {
                CompetitionId = Convert.ToInt32(competitionId);
            }

            await Task.CompletedTask;
        }
    }
}
