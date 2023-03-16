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
    public partial class AddPassageTime
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        protected IEnumerable<GateWithTimePassage> gateWithTimePassage = new List<GateWithTimePassage>();
        protected GateWithTimePassage gateWithTimePassageModel = new GateWithTimePassage { IsActive = true };
        protected IEnumerable<Competitioner> CompetitionersForSelect = new List<Competitioner>();
        protected IEnumerable<GateWithTime> GateWithTimeForSelect = new List<GateWithTime>();

        [Parameter]
        public int IdTrackFromPage { get; set; }

        [Parameter]
        public int IdCompetitionFromPage { get; set; }

        [Parameter]
        public int GateTypeOnPage { get; set; }
        public int GateWihtTimeId { get; set; }
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

            switch (GateTypeOnPage)
            {
                case 1:
                    GateWithTimeForSelect = await context.GateWithTimes
                      .Where(x => x.IsActive && x.Type == GateType.StartingGate && x.TrackId == IdTrackFromPage)
                      .ToListAsync();                    
                    break;
                case 2:
                    GateWithTimeForSelect = await context.GateWithTimes
                      .Where(x => x.IsActive && x.Type == GateType.FinishGate && x.TrackId == IdTrackFromPage)
                      .ToListAsync();
                    break;

                case 3:
                    GateWithTimeForSelect = await context.GateWithTimes
                      .Where(x => x.IsActive && x.Type == GateType.TimeGate && x.Id == IdTrackFromPage)
                      .ToListAsync();
                    break;
            }

            gateWithTimePassageModel.GateWihtTimeId = GateWithTimeForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithTimePassageModel.CompetitionerId = CompetitionersForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            GateWihtTimeId = GateWithTimeForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            await ResetDataToDefaultAsync();
        }

        private async Task SavePassageTime()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimePassageModel.Id is 0)
            {
                gateWithTimePassageModel.IsActive = true;
                gateWithTimePassageModel.GateWihtTimeId = GateWihtTimeId;
                await context.GateWithTimePassages.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassages.Update(gateWithTimePassageModel);
            }

            await context.SaveChangesAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task SavePasseageNow()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimePassageModel.Id is 0)
            {
                gateWithTimePassageModel.IsActive = true;
                gateWithTimePassageModel.GateWihtTimeId = GateWihtTimeId;
                gateWithTimePassageModel.GatePasssage = DateTime.Now;
                await context.GateWithTimePassages.AddAsync(gateWithTimePassageModel);
            }
            else
            {
                context.GateWithTimePassages.Update(gateWithTimePassageModel);
            }

            await context.SaveChangesAsync();
           await ResetDataToDefaultAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimePassageModel = new GateWithTimePassage
            {

                GatePasssage = DateTime.Now,

                CompetitionerId = CompetitionersForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };
            await Task.CompletedTask;
        }
    }

}

