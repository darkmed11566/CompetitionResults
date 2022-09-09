using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddGateWichTimePasseges
    {
        private IEnumerable<GateWithTimePassage> gateWithTimePassage = new List<GateWithTimePassage>();

        private DateTime newPassage;

        protected override void OnInitialized()
        {
            gateWithTimePassage = gateWithTimePassageRepository.GetAll();

        }



        private void DeleteGateWithTimePassage(GateWithTimePassage deletedGateWithTimePassage)
        {
            gateWithTimePassageRepository.Remove(deletedGateWithTimePassage);
        }

        private void AddGateWithTimePassage()
        {

            var dbGateWithTimePassage = new GateWithTimePassage();

            dbGateWithTimePassage.GatePasssage = DateTime.Now;
            dbGateWithTimePassage.IsActive = true;

            gateWithTimePassageRepository.Save(dbGateWithTimePassage);

        }
    }
}
