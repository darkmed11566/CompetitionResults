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
    public partial class AddPassageOnSector
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }
        protected IEnumerable<GateWithPenaltyPassage> gateWithPenaltyPassage = new List<GateWithPenaltyPassage>();
        protected GateWithPenaltyPassage gateWithPenaltyPassageModel = new GateWithPenaltyPassage { IsActive = true };
        protected IEnumerable<GateWithPenalty> GateWithPenaltiesOnSector = new List<GateWithPenalty>();
        protected IEnumerable<Competitioner> CompetitionersForSelect = new List<Competitioner>();
        protected IEnumerable<GateWithPenalty> GateWithPenaltiesForSelect = new List<GateWithPenalty>();
        protected IEnumerable<Penalties> PenaltiesForSelect = new List<Penalties>();

        [Parameter]
        public int IdSectorFromPage { get; set; }
        [Parameter]
        public int IdCompetitionFromPage { get; set; }
        protected override async Task OnInitializedAsync()
        {           
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();           

            CompetitionersForSelect = await context.Competitioners
               .AsNoTracking()
               .Where(x => x.IsActive 
               //&& x.StatusInTrack == StatusSportsmanInTrack.OnTrack
              /* &&x.CompetitionId==IdCompetitionFromPage*/)
               .ToListAsync();           

            gateWithPenaltyPassageModel.GateWihtPenaltyId = GateWithPenaltiesForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            gateWithPenaltyPassageModel.CompetitionerId = CompetitionersForSelect
               .OrderBy(x => x.Id)
               .FirstOrDefault()?.Id ?? 0;

            PenaltiesForSelect = Enum.GetValues(typeof(Penalties))
               .OfType<Penalties>()
               .ToList();

            GateWithPenaltiesOnSector = await context.GateWithPenalties
              .AsNoTracking()
              .Where(x => x.IsActive/* && x.SectorId == IdSectorFromPage*/)
              .ToListAsync();

            await ResetDataToDefaultAsync();
        }

        private async Task SavePenaltyOnSector()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithPenaltyPassageModel.Id is 0)
            {
               
                await context.GateWithPenaltiePassages.AddAsync(gateWithPenaltyPassageModel);
            }
            else
            {
                context.GateWithPenaltiePassages.Update(gateWithPenaltyPassageModel);
            }
            await context.SaveChangesAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyPassageModel = new GateWithPenaltyPassage
            {
                IsActive = true,

                PenaltyOnGate = Penalties.CleanPassage,

                GateWihtPenaltyId = GateWithPenaltiesForSelect
                  .OrderBy(x => x.Id)
                     .FirstOrDefault()
                     ?.Id ?? 0,

                CompetitionerId = CompetitionersForSelect
                .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await Task.CompletedTask;
        }

    }
}

