using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddGates
    {
        private IEnumerable<GateWithTime> gateWithTime = new List<GateWithTime>();
        private IEnumerable<GateWithPenalty> gateWithPenalty = new List<GateWithPenalty>();

        private GateNameWithPenalty newGateNumber;
        private GateNameWithTime newGateName;

        protected override void OnInitialized()
        {
            gateWithTime = gateWithTimeRepository.GetAll();
            gateWithPenalty = gateWithPenaltyRepository.GetAll();
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
        private void AddBackGateWithPenalty()
        {

            var dbBackGate = new GateWithPenalty();

            dbBackGate.Type = GateType.BackGate;
            dbBackGate.GateNumber = newGateNumber;
            dbBackGate.IsActive = true;

            gateWithPenaltyRepository.Save(dbBackGate);

        }
        private void AddStraightGateWithPenalty()
        {

            var dbStraightGate = new GateWithPenalty();

            dbStraightGate.Type = GateType.StraightGate;
            dbStraightGate.GateNumber = newGateNumber;
            dbStraightGate.IsActive = true;

            gateWithPenaltyRepository.Save(dbStraightGate);

        }
    }
}
