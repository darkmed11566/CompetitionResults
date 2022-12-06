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

        private IEnumerable<GateWithTime> gateWithTime = new List<GateWithTime>();
        private IEnumerable<GateWithPenalty> gateWithPenalty = new List<GateWithPenalty>();

        private GateNameWithPenalty newGateNumber;
        private GateNameWithTime newGateName;
        private IEnumerable<Sector> SectorsForSelect = new List<Sector>();
        private long newSector;
        protected override void OnInitialized()
        {
            gateWithTime = gateWithTimeRepository.GetAll();
        }

        protected override async Task OnInitializedAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            gateWithPenalty = await context.GateWithPenalties.AsNoTracking().ToListAsync();
            SectorsForSelect = await context.Sectors.AsNoTracking().ToListAsync();
            newSector = SectorsForSelect.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
        }


        private void DeleteGateWithTime(GateWithTime deletedGatewithTime)
        {
            gateWithTimeRepository.Remove(deletedGatewithTime);
        }
        private void DeleteGateWithPenalty(GateWithPenalty deletedGateWithPenalty)
        {
            gateWithPenaltyRepository.Remove(deletedGateWithPenalty);
        }
        private void AddGateWithTime()
        {

            var dbGateWithTime = new GateWithTime();

            dbGateWithTime.Type = GateType.TimeGate;
            dbGateWithTime.GateName = newGateName;
            dbGateWithTime.IsActive = true;

            gateWithTimeRepository.Save(dbGateWithTime);

        }
        private async Task AddBackGateWithPenalty()
        {

            var dbBackGate = new GateWithPenalty();

            dbBackGate.Type = GateType.BackGate;
            dbBackGate.GateNumber = newGateNumber;
            dbBackGate.IsActive = true;
            dbBackGate.SectorId = newSector;

            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.GateWithPenalties.AddAsync(dbBackGate);
            await context.SaveChangesAsync();
        }
        private async Task AddStraightGateWithPenalty()
        {

            var dbStraightGate = new GateWithPenalty();

            dbStraightGate.Type = GateType.StraightGate;
            dbStraightGate.GateNumber = newGateNumber;
            dbStraightGate.IsActive = true;
            dbStraightGate.SectorId = newSector;

            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.GateWithPenalties.AddAsync(dbStraightGate);
            await context.SaveChangesAsync();

        }

        protected void SectorSelected(ChangeEventArgs args)
        {
            var x = args.Value;
        }

    }
}
