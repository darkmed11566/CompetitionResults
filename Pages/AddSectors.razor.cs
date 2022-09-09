using System.Collections.Generic;
using CompetitionResults.Data;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;

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

        private IEnumerable<Sector>
        sector = new List<Sector>();

        protected override void OnInitialized()
        {
            sector = sectorRepository.GetAll();

        }

        private int newSectorNumber;

        private void AddSector()
        {

            var dbSector = new Sector();

            dbSector.SectorNumber = newSectorNumber;
            dbSector.IsActive = true;

            sectorRepository.Save(dbSector);

        }

        private void DeleteSector(long id)
        {
            sectorRepository.Remove(id);
        }







    }
}

