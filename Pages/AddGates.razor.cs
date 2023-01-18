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
    public partial class AddGates
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<GateWithTime> gateWithTime = new List<GateWithTime>();
        protected IEnumerable<GateWithPenalty> gateWithPenalty = new List<GateWithPenalty>();

        protected GateWithPenalty gateWithPenaltyModel = new GateWithPenalty();
        protected GateWithTime gateWithTimeModel = new GateWithTime();


        protected IEnumerable<Sector> SectorsForSelect = new List<Sector>();
        protected IEnumerable<Track> TracksForSelect = new List<Track>();

        protected IEnumerable<GateType> GateTypesForSelect = new List<GateType>();
        protected IEnumerable<GateNameWithPenalty> GateNamesWithPenaltyForSelect = new List<GateNameWithPenalty>();
        protected IEnumerable<GateNameWithTime> GateNamesWithTimeForSelect = new List<GateNameWithTime>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            SectorsForSelect = await context.Sectors
                .AsNoTracking()
                .Where(x => !x.IsFull && x.IsActive)
                .ToListAsync();

            await ResetDataToDefaultGateWithPenaltyAsync();

            TracksForSelect = await context.Tracks
                .AsNoTracking()
                .Where(x => !x.IsFull && x.IsActive)
                .ToListAsync();

            await ResetDataToDefaultGateWithTimeAsync();

            GateTypesForSelect = Enum.GetValues(typeof(GateType))
               .OfType<GateType>()
               .ToList();

            GateNamesWithPenaltyForSelect = Enum.GetValues(typeof(GateNameWithPenalty))
               .OfType<GateNameWithPenalty>()               
               .ToList();

            GateNamesWithTimeForSelect = Enum.GetValues(typeof(GateNameWithTime))
              .OfType<GateNameWithTime>()
              .ToList();
    }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTime = await context.GateWithTimes
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();

            gateWithPenalty = await context.GateWithPenalties
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();   

        }

        private void EditGateWithPenalty(GateWithPenalty gateWithPenaltyToEdit)
        {
            var shallowCopy = new GateWithPenalty
            {
                Id = gateWithPenaltyToEdit.Id,
                IsActive = gateWithPenaltyToEdit.IsActive,
                GateNumber = gateWithPenaltyToEdit.GateNumber,
                SectorId = gateWithPenaltyToEdit.SectorId,
                Type = gateWithPenaltyToEdit.Type

            };

            gateWithPenaltyModel = shallowCopy;
        }

        private async Task DeleteGateWithPenaltyAsync(GateWithPenalty gateWithPenaltyToDelete)
        {
            gateWithPenaltyToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithPenalties.Update(gateWithPenaltyToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private void EditGateWithTime(GateWithTime gateWithTimeToEdit)
        {
            var shallowCopy = new GateWithTime
            {
                Id = gateWithTimeToEdit.Id,
                IsActive = gateWithTimeToEdit.IsActive,
                GateName = gateWithTimeToEdit.GateName,
                TrackId = gateWithTimeToEdit.TrackId,
                Type = gateWithTimeToEdit.Type
            };

            gateWithTimeModel = shallowCopy;
        }

        private async Task DeleteGateWithTimeAsync(GateWithTime gateWithTimeToDelete)
        {
            gateWithTimeToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.GateWithTimes.Update(gateWithTimeToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task SaveGateWithTime()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithTimeModel.Id is 0)
            {

                await context.GateWithTimes.AddAsync(gateWithTimeModel);
            }
            else
            {
                context.GateWithTimes.Update(gateWithTimeModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();

            await ResetDataToDefaultGateWithTimeAsync();
        }

        private async Task SaveGateWithPenalty()
        {

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (gateWithPenaltyModel.Id is 0)
            {

                await context.GateWithPenalties.AddAsync(gateWithPenaltyModel);
            }
            else
            {
                context.GateWithPenalties.Update(gateWithPenaltyModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultGateWithPenaltyAsync();
            
        }

        private async Task ResetDataToDefaultGateWithPenaltyAsync()

        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenaltyModel = new GateWithPenalty
            {
                IsActive = true,
                Type = GateType.StraightGate,
                GateNumber = GateNameWithPenalty.Gate1,
                SectorId = SectorsForSelect
                  .OrderBy(x => x.Id)
                  .FirstOrDefault()
                  ?.Id ?? 0
            };

            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultGateWithTimeAsync()

        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithTimeModel = new GateWithTime
            {
                IsActive = true,
                Type = GateType.TimeGate,
                GateName = GateNameWithTime.Point1,
                TrackId = TracksForSelect
                    .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }
    }
}