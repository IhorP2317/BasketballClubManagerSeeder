using BasketballClubManagerSeeder.Mappers;
using BasketballClubManagerSeeder.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BasketballClubManagerSeeder.Data {
    public class DataImporter {
        private ExoftBasketballClubContext _context;

        public DataImporter(ExoftBasketballClubContext context) {
            _context = context;
        }
        public async Task ImportData(string teamFilePath, string playerFilePath, string coachFilePath, string playerExperienceFilePath, string matchFilePath) {
            var teams =  ReadCsvTeams(teamFilePath);
            await _context.Teams.AddRangeAsync(teams);
            await _context.SaveChangesAsync();
            var coaches = await ReadCsvCoaches(coachFilePath);
            var players = await  ReadCsvPlayers(playerFilePath);
            var playerExperiences = await ReadCsvPlayerExperiences (playerExperienceFilePath);
            var matches = await ReadCsvMatches(matchFilePath);


            await _context.Coaches.AddRangeAsync(coaches);
            await _context.SaveChangesAsync();
            await _context.Players.AddRangeAsync(players);
            await _context.SaveChangesAsync();
            await _context.PlayerExperiences.AddRangeAsync(playerExperiences);
            await _context.SaveChangesAsync();
            await _context.Matches.AddRangeAsync(matches);
            await _context.SaveChangesAsync();

        }


        

        private List<Team> ReadCsvTeams(string filePath) {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Context.RegisterClassMap<TeamMap>();
                return csv.GetRecords<Team>().ToList();
            }
        }
        private async Task<List<Player>> ReadCsvPlayers(string filePath) {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Context.RegisterClassMap<PlayerMap>();

                var players = csv.GetRecords<Player>().ToList();
                foreach (var player in players) {
                    var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == player.TeamId);
                    player.Team = team;
                }
                return players;
            }
        }
        private async Task<List<Coach>> ReadCsvCoaches(string filePath) {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Context.RegisterClassMap<CoachMap>();

                var coaches = csv.GetRecords<Coach>().ToList();
                foreach (var coach in coaches) {
                    var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == coach.TeamId);
                    coach.Team = team;
                }
                return coaches;
            }
        }
        private async Task<List<PlayerExperience>> ReadCsvPlayerExperiences(string filePath) {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Context.RegisterClassMap<PlayerExperienceMap>();

                var playerExperiences = csv.GetRecords<PlayerExperience>().ToList();
                foreach (var playerExperience in playerExperiences) {
                    var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == playerExperience.TeamId);
                    var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerExperience.PlayerId);
                    playerExperience.Team = team;
                    playerExperience.Player = player;
                }
                return playerExperiences;
            }
        }
        private async Task<List<Match>> ReadCsvMatches(string filePath) {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Context.RegisterClassMap<MatchMap>();

                var matches = csv.GetRecords<Match>().Where(m => m.AwayTeamId != m.HomeTeamId).ToList();
                foreach (var match in matches) {
                    var homeTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == match.HomeTeamId);
                    var awayTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == match.AwayTeamId);
                    match.HomeTeam = homeTeam;
                    match.AwayTeam = awayTeam;
                }
                return matches;
            }
        }
        public void CheckTeamIds(string teamFilePath, string coachFilePath) {
            // Read Team IDs from teams.csv
            List<Guid> teamIds = new List<Guid>();
            using (var reader = new StreamReader(teamFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read()) {
                    var id = csv.GetField<Guid>("id");
                    teamIds.Add(id);
                }
            }

            // Read Team IDs from coaches.csv and compare
            using (var reader = new StreamReader(coachFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read()) {
                    var teamId = csv.GetField<Guid>("TeamId");
                    if (!teamIds.Contains(teamId)) {
                        Console.WriteLine($"Mismatch found: No matching TeamId for {teamId} in teams.csv");
                    }
                }
            }
        }
        public async Task GenerateStatisticsForMatches() {
            var matches = await _context.Matches.ToListAsync();
            var playerExperiences = await _context.PlayerExperiences.ToListAsync();

            var statistics = new List<Statistic>();
            Random rnd = new Random();

            foreach (var match in matches) {
                var matchDate = match.StartTime.Date;

               
                var homeTeamPlayerExperiences = playerExperiences
                    .Where(pe => matchDate >= pe.StartDate.ToDateTime(TimeOnly.MinValue) &&
                                 (!pe.EndDate.HasValue || matchDate <= pe.EndDate.Value.ToDateTime(TimeOnly.MinValue)) &&
                                 pe.TeamId == match.HomeTeamId)
                    .OrderBy(pe => pe.CreatedTime)
                    .Take(10)
                    .ToList();

               
                var awayTeamPlayerExperiences = playerExperiences
                    .Where(pe => matchDate >= pe.StartDate.ToDateTime(TimeOnly.MinValue) &&
                                 (!pe.EndDate.HasValue || matchDate <= pe.EndDate.Value.ToDateTime(TimeOnly.MinValue)) &&
                                 pe.TeamId == match.AwayTeamId)
                    .OrderBy(pe => pe.CreatedTime)
                    .Take(10)
                    .ToList();

               
                var validPlayerExperiences = homeTeamPlayerExperiences.Concat(awayTeamPlayerExperiences).ToList();

                foreach (var playerExperience in validPlayerExperiences) {
                    for (int i = 0; i < 4; i++) {
                        var statistic = new Statistic {
                            MatchId = match.Id,
                             PlayerExperienceId = playerExperience.Id,
                             TimeUnit = i + 1,
                             OnePointShotHitCount = rnd.Next(2),
                             OnePointShotMissCount = rnd.Next(2),
                             TwoPointShotHitCount = rnd.Next(2),
                             TwoPointShotMissCount = rnd.Next(2),
                            ThreePointShotHitCount = rnd.Next(2),
                             ThreePointShotMissCount = rnd.Next(2),
                             AssistCount = rnd.Next(2),
                             OffensiveReboundCount = rnd.Next(2),
                             DefensiveReboundCount = rnd.Next(2),
                             StealCount = rnd.Next(2),
                             BlockCount = rnd.Next(2),
                             TurnoverCount = rnd.Next(2),
                             CourtTime = GenerateRandomCourtTime(),
                             Match = match,
                             PlayerExperience = playerExperience
                        };
                        if (!await _context.Statistics.AnyAsync(s => s.MatchId == statistic.MatchId && s.PlayerExperienceId == statistic.PlayerExperienceId && s.TimeUnit == statistic.TimeUnit))
                            statistics.Add(statistic);
                    }
                }
            }

            await _context.Statistics.AddRangeAsync(statistics);
            await _context.SaveChangesAsync();
        }

       


        public TimeOnly GenerateRandomCourtTime() {
            Random random = new Random();

            int minutes = random.Next(0, 12);
            int seconds = random.Next(0, 60);

            return new TimeOnly(0, minutes, seconds);
        }

    }
}
