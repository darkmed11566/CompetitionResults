using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class AddTracks
    {

        private IEnumerable<Track> track = new List<Track>();
        private IEnumerable<GateWithTime> gate = new List<GateWithTime>();


        protected override void OnInitialized()
        {
            track = trackRepository.GetAll();
        }

        private TrackType newType;
        private Competition newNameOfCompetition;


        private void AddTrack()
        {
            var dbTrack = new Track();

            dbTrack.TrackType = newType;

            dbTrack.IsActive = true;

            //dbTrack.NameOfCompetition = newNameOfCompetition;

            trackRepository.Save(dbTrack);

            var dbGateWithTimeStart = new GateWithTime();

            dbGateWithTimeStart.Type = GateType.StartingGate;

            dbGateWithTimeStart.GateName = GateNameWithTime.Start;

            dbGateWithTimeStart.IsActive = true;

            gateWithTimeRepository.Save(dbGateWithTimeStart);

            var dbGateWithTimeFinish = new GateWithTime();

            dbGateWithTimeFinish.Type = GateType.FinisGate;

            dbGateWithTimeFinish.GateName = GateNameWithTime.Finish;

            dbGateWithTimeFinish.IsActive = true;

            gateWithTimeRepository.Save(dbGateWithTimeFinish);

        }



        private void DeleteTrack(long id)
        {
            trackRepository.Remove(id);
        }
    }
}
