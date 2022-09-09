using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddGateWichPenaltyPasseges
    {
        private IEnumerable<GateWithPenaltyPassage> gateWithPenaltyPassage = new List<GateWithPenaltyPassage>();

        private Penalties newPenalty;


        protected override void OnInitialized()
        {
            gateWithPenaltyPassage = gateWithPenaltyPassageRepository.GetAll();
        }

        private void DeleteGateWithPenaltyPassage(GateWithPenaltyPassage deletedGateWithPenaltyPassage)
        {
            gateWithPenaltyPassageRepository.Remove(deletedGateWithPenaltyPassage);
        }

        private void AddGateWithPenaltyPassage()
        {

            var dbGateWithPenaltyPassage = new GateWithPenaltyPassage();

            dbGateWithPenaltyPassage.PenaltyOnGate = newPenalty;
            dbGateWithPenaltyPassage.IsActive = true;

            gateWithPenaltyPassageRepository.Save(dbGateWithPenaltyPassage);

        }
    }
}
