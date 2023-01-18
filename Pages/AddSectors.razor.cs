using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompetitionResults.Data;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompetitionResults.Pages
{
    public partial class AddSectors
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        protected IEnumerable<Sector> sectors = new List<Sector>();
        protected Sector sectorModel = new Sector { IsActive = true };

        protected IEnumerable<Track> TracksForSelect = new List<Track>();

        protected override async Task OnInitializedAsync()
        {
            await RenewStateAsync();

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            TracksForSelect = await context
                .Tracks.AsNoTracking()
                .Where(x => !x.IsFull && x.IsActive)
                .ToListAsync();

            sectorModel.TrackId = TracksForSelect
                .OrderBy(x => x.Id).
                FirstOrDefault()?.Id ?? 0;

            await ResetDataToDefaultAsync();
        }



        private async Task RenewStateAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            sectors = await context.Sectors
                .AsNoTracking()
                .Where(t => t.IsActive)
                .ToListAsync();
        }


        private async Task SaveSector()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            if (sectorModel.Id is 0)
            {

                await context.Sectors.AddAsync(sectorModel);
            }
            else
            {
                context.Sectors.Update(sectorModel);
            }

            await context.SaveChangesAsync();
            await RenewStateAsync();
            await ResetDataToDefaultAsync();

        }

        private void EditSector(Sector sectorToEdite)
        {
            var shallowCopy = new Sector
            {
                Id = sectorToEdite.Id,
                Number = sectorToEdite.Number,
                TrackId = sectorToEdite.TrackId,
                IsActive = sectorToEdite.IsActive
            };

            sectorModel = shallowCopy;
        }

        private async Task DeleteSectorAsync(Sector sectorToDelete)
        {
            sectorToDelete.IsActive = false;

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            context.Sectors.Update(sectorToDelete);
            await context.SaveChangesAsync();
            await RenewStateAsync();
        }

        private async Task ResetDataToDefaultAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            sectorModel = new Sector
            {
                IsFull = false,
                IsActive = true,
                Number = 1,
                TrackId = TracksForSelect
                 .OrderBy(x => x.Id)
                    .FirstOrDefault()
                    ?.Id ?? 0
            };

            await RenewStateAsync();
        }


    }
}

