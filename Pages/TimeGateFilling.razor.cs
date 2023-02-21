using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class TimeGateFilling
    {
        [Inject]
        protected NavigationManager NavManager { get; set; }

        [Parameter]
        public int GateId { get; set; }

        protected override async Task OnInitializedAsync()
        {
           
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("GateId", out var gateId))
            {
                GateId = Convert.ToInt32(gateId);
            }

            await Task.CompletedTask;

        }

    }
}
