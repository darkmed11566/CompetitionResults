using CompetitionResults.Data;
using CompetitionResults.EnumsAndConstants;
using CompetitionResults.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetitionResults.Pages
{
    public partial class ViewResults
    {
        [Inject]
        protected IServiceScopeFactory serviceScopeFactory { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected IEnumerable<Competitioner> KM1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> KW1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> CM1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> CW1 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MW = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MM = new List<Competitioner>();
        protected IEnumerable<Competitioner> K1M3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> K1W3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C1M3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C1W3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MW3 = new List<Competitioner>();
        protected IEnumerable<Competitioner> C2MM3 = new List<Competitioner>();
        protected IEnumerable<GateWithPenalty> gateWithPenalties = new List<GateWithPenalty>();
        protected IEnumerable<Sector> sectorsInTrack = new List<Sector>();

        protected IEnumerable<Track> tracks = new List<Track>();

        protected List<ResultsViewModel> Results { get; set; } = new List<ResultsViewModel>();

        [Parameter]
        public int TrackId { get; set; }
        [Parameter]
        public int CompetitionId { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("TrackId", out var trackId))
            {
                TrackId = Convert.ToInt32(trackId);
            }

            var uriC = NavManager.ToAbsoluteUri(NavManager.Uri);
            if (QueryHelpers.ParseQuery(uriC.Query).TryGetValue("CompetitionId", out var competitionId))
            {
                CompetitionId = Convert.ToInt32(competitionId);
            }

            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebContext>();

            gateWithPenalties = await context.GateWithPenalties
                .AsNoTracking()
                .Include(x => x.Sector).ThenInclude(x => x.Track)
                .Where(x => x.IsActive && x.Sector.Track.Id == TrackId)
                .ToListAsync();

            KM1 = await context.Competitioners
                 .AsNoTracking()
                 .Include(x => x.Sportsman)
                    .ThenInclude(x=>x.Coach)
                 .Include(x => x.GateWithPenaltyPassages)
                    .ThenInclude(x=>x.PenaltyGate)
                 .Include(x => x.GateWithTimePassages)
                     .ThenInclude(x => x.TimeGate)
                 .Where(x => x.IsActive
                 && x.BoatClass == BoatClasses.K1M
                 && x.CompetitionId == CompetitionId)
                 .ToListAsync();


            foreach (var item in KM1)
            {
                DateTime startTime = new DateTime();
                DateTime finishTime = new DateTime();
                TimeSpan timeInTrack = new TimeSpan();
                decimal totalPenalty = 0;
                var penaltyOnGate = new List<(GateNameWithPenalty Name, int Value)>();

                foreach (var time in item.GateWithTimePassages)
                {
                    if (time.TimeGate.Type == EnumsAndConstants.GateType.StartingGate)
                    {
                        startTime = time.GatePasssage;
                    }
                }

                foreach (var time in item.GateWithTimePassages)
                {
                    if (time.TimeGate.Type == EnumsAndConstants.GateType.FinishGate)
                    {
                        finishTime = time.GatePasssage;
                    }
                }

                var min = item.GateWithTimePassages
                    .FirstOrDefault(x => x.TimeGate.Type == EnumsAndConstants.GateType.StartingGate)
                    .GatePasssage;
                var max = item.GateWithTimePassages
                    .FirstOrDefault(x => x.TimeGate.Type == EnumsAndConstants.GateType.FinishGate)
                    .GatePasssage;
                timeInTrack = max.Subtract(min);
                var timeInTrackDouble = ((decimal)timeInTrack.TotalSeconds);
                var timeInTrackDouble2 = Math.Round(timeInTrackDouble, 2);

                foreach (var penaltyPassage in item.GateWithPenaltyPassages)
                {
                    if (penaltyPassage.PenaltyOnGate == EnumsAndConstants.Penalties.WrongPassageOrSkip)
                    {
                        totalPenalty = totalPenalty + 50;
                    }
                    else
                    if (penaltyPassage.PenaltyOnGate == EnumsAndConstants.Penalties.PassageWichTouch)
                    {
                        totalPenalty = totalPenalty + 2;
                    }
                }

                decimal totalTime = timeInTrackDouble2 + totalPenalty;

                foreach (var penalty in item.GateWithPenaltyPassages)
                {
                                       
                        var x = penalty.PenaltyOnGate switch
                        {
                            EnumsAndConstants.Penalties.PassageWichTouch => 2,
                            EnumsAndConstants.Penalties.WrongPassageOrSkip => 50,
                            _ => 0

                        };

                        penaltyOnGate.Add((penalty.PenaltyGate.GateNumber, x));
                   
                }


                Results.Add(new ResultsViewModel()
                {
                    Number = item.Number,
                    SecondName = item.Sportsman.SecondName,
                    Name = item.Sportsman.Name,
                    Country = item.Sportsman.Country,
                    Rang = item.Sportsman.Rang,
                    DateOfBirth = item.Sportsman.DateOfBirth,
                    ClubName = item.Sportsman.ClubName,
                    CoachName = item.Sportsman.Coach.SecondName,
                    StartTime = startTime,
                    FinishTime = finishTime,
                    TimePassageTrack = timeInTrackDouble2,
                    TotolPenalty = totalPenalty,
                    TotolTime = totalTime,
                    PenaltesOnGate = penaltyOnGate
                }
                );
            }
        }
    }

    public class ResultsViewModel
    {
        public int Number { get; set; }
        public string SecondName { get; set; }
        public string Name { get; set; }
        public ListOfCountries Country { get; set; }
        public Rangs Rang { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ClubName { get; set; }
        public string CoachName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public decimal TimePassageTrack { get; set; }
        public decimal TotolPenalty { get; set; }
        public decimal TotolTime { get; set; }
        public int Plase { get; set; }
        public decimal LostFromFirstPlace { get; set; }
        public List<(GateNameWithPenalty Name, int Value)> PenaltesOnGate { get; set; }

    }
}
