﻿using CompetitionResults.Data;
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
    public partial class FinishTimeFilling
    {
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Parameter]
        public int TrackId { get; set; }
        [Parameter]
        public int CompetitionId { get; set; }
        //[Parameter]
        //public string TrackIdStr { get; set; }
        //[Parameter]
        //public string CompetitionIdStr { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //var x = int.TryParse(TrackIdStr, out var trackId);
            //var y = int.TryParse(CompetitionIdStr, out var competitionId);

            //TrackId = trackId;
            //CompetitionId = competitionId;

            //await Task.Delay(10);
            var uriT = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uriT.Query).TryGetValue("TrackId", out var trackId))
            {
                TrackId = Convert.ToInt32(trackId);
            }

            var uriC = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uriC.Query).TryGetValue("CompetitionId", out var competitionId))
            {
                CompetitionId = Convert.ToInt32(competitionId);
            }
            await Task.CompletedTask;
            //StateHasChanged();
        }
    }
}
