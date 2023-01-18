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
    public partial class AddTracks
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Track> tracks = new List<Track>();

        protected Track trackModel = new Track { IsActive = true };

        protected IEnumerable<Competition> CompetitionsForSelect = new List<Competition>();
        protected IEnumerable<TrackType> TrackTypesForSelect = new List<TrackType>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            CompetitionsForSelect = await context.Competitions
                .AsNoTracking()
                .Where(x => !x.IsFull && x.IsActive)
                .ToListAsync();

            trackModel.CompetitionId = CompetitionsForSelect
                .OrderBy(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;

            TrackTypesForSelect = Enum.GetValues(typeof(TrackType))
                .OfType<TrackType>()
                .ToList();

            await ResetDataToDefaultAsync();
        }

        private async Task SaveTrack()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (trackModel.Id is 0)
            {
                List<GateWithTime> gatesForTrackModel = new List<GateWithTime>
                {
                    new GateWithTime
                    {
                        Type = GateType.StartingGate,
                        GateName = GateNameWithTime.Start,
                        IsActive = true
                    },
                    new GateWithTime
                    {
                        Type = GateType.FinisGate,
                        GateName = GateNameWithTime.Finish,
                        IsActive = true
                    }
                };

                trackModel.Gates = gatesForTrackModel;

                await context.Tracks.AddAsync(trackModel);
            }
            else
            {
                context.Tracks.Update(trackModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();
        }

        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            tracks = await context.Tracks
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        private void EditTrack(Track trackToEdit)
        {
            var shallowCopy = new Track
            {
                Id = trackToEdit.Id,
                IsFull = trackToEdit.IsFull,
                CompetitionId = trackToEdit.CompetitionId,
                TrackType = trackToEdit.TrackType,
                IsActive = trackToEdit.IsActive
            };

            trackModel = shallowCopy;
        }

        private async Task DeleteTrackAsync(Track trackToDelete)
        {
            trackToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Tracks.Update(trackToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            trackModel = new Track
            {
                IsFull = false,
                IsActive = true,
                TrackType = TrackType.Run1,
                 CompetitionId = CompetitionsForSelect
                 .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }
    }
}
