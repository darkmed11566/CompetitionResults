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
        //[Inject] CompetitionResults.Data.Repository.SectorRepository sectorRepository { get; set; }
       

        //{
        // WebContext.Remove();
        //}

        //public static class WebContextExtensions
        //{
        //public static void Remove(this WebContext context,object o)
        //        o.IsActive = false;
        //        context.SaveChanges();
        //}
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }


        private IEnumerable<Sector> sector = new List<Sector>();

        protected override async Task OnInitializedAsync()
        {
            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            sector = await  context.Sectors.AsNoTracking().ToListAsync();
            TracksForSelect = await context.Tracks.AsNoTracking().ToListAsync();
            newTrack = TracksForSelect.OrderBy(x => x.Id).FirstOrDefault()?.Id ?? 0;
        }

        private int newSectorNumber;
        private long newTrack;
        private IEnumerable<Track> TracksForSelect = new List<Track>();
        private async Task AddSector()
        {

            var dbSector = new Sector();

            dbSector.Number = newSectorNumber;
            dbSector.IsActive = true;
            dbSector.TrackId = newTrack;


            using var scope = serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<WebContext>();
            await context.Sectors.AddAsync(dbSector);
            await context.SaveChangesAsync();

            

        }

        protected void TrackSelected(ChangeEventArgs args) 
        {
            var x = args.Value;
        }


        private void DeleteSector(long id)
        {
            //sectorRepository.Remove(id);
        }







    }
}

