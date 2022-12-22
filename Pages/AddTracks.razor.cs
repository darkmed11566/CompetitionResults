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

        private IEnumerable<Track> track = new List<Track>();
        private IEnumerable<GateWithTime> gate = new List<GateWithTime>();
     
        private TrackType newType;
        private long newCompetition;
        private IEnumerable<Competition> CompetitionsForSelect = new List<Competition>();
        private bool newIsFull;
        private long Id;
        protected override async Task OnInitializedAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            track = await context.Tracks.AsNoTracking().ToListAsync();
            CompetitionsForSelect = await context.Competitions.AsNoTracking()
                .Where (x=>x.IsFull==false&&x.IsActive==true).ToListAsync();
            newCompetition = CompetitionsForSelect.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
        }

        private async Task AddTrack()
        {
            var dbTrack = new Track();

            dbTrack.TrackType = newType;
            dbTrack.IsActive = true;
            dbTrack.CompetitionId = newCompetition;
            dbTrack.IsFull = newIsFull;

            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.Tracks.AddAsync(dbTrack);
            await context.SaveChangesAsync();
            Id = dbTrack.Id;
            await AddStartGate();
            await AddFinishGate();

        }

        private async Task AddStartGate()
        {

            var dbGateWithTimeStart = new GateWithTime();

            dbGateWithTimeStart.Type = GateType.StartingGate;
            dbGateWithTimeStart.GateName = GateNameWithTime.Start;
            dbGateWithTimeStart.IsActive = true;
            dbGateWithTimeStart.TrackId = Id;
            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.GateWithTimes.AddAsync(dbGateWithTimeStart);
            await context.SaveChangesAsync();
            
        }
        private async Task AddFinishGate()
        {  
            var dbGateWithTimeFinish = new GateWithTime();
            dbGateWithTimeFinish.Type = GateType.FinisGate;
            dbGateWithTimeFinish.GateName = GateNameWithTime.Finish;
            dbGateWithTimeFinish.IsActive = true;
            dbGateWithTimeFinish.TrackId = Id;

            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.GateWithTimes.AddAsync(dbGateWithTimeFinish);
            await context.SaveChangesAsync();
        }
        protected void TrackSelected(ChangeEventArgs args)
        {
            var x = args.Value;
        }
        protected void CompetitionSelect(ChangeEventArgs args)
        {
            var x = args.Value;
        }

        private void DeleteTrack(long id)
        {
            trackRepository.Remove(id);
        }
    }
}
