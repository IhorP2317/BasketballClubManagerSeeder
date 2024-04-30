using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballClubManagerSeeder.Models;
using CsvHelper.Configuration;

namespace BasketballClubManagerSeeder.Mappers {
    public class MatchMap: ClassMap<Match> {
        MatchMap()
        {
            Map(m => m.Id).Convert(args =>
            {
                var idString = args.Row.GetField("Id");
                return Guid.TryParse(idString, out Guid id) ? id : Guid.Empty;
            });
            Map(m => m.Location).Name("Location");
            Map(m => m.Status).Name("Status");
            Map(m => m.SectionCount).Name("SectionCount");
            Map(m => m.RowCount).Name("RowCount");
            Map(m => m.SeatCount).Name("SeatCount");
            Map(m => m.HomeTeamId).Convert(args => Guid.Parse(args.Row.GetField("HomeTeamId"))); 
            Map(m => m.AwayTeamId).Convert(args => Guid.Parse(args.Row.GetField("AwayTeamId")));
            Map(m => m.StartTime).Convert(args => DateTime.Parse(args.Row.GetField("StartTime")));
            Map(m => m.EndTime).Convert(args => DateTime.Parse(args.Row.GetField("EndTime")));
            Map(m => m.CreatedTime).Convert(args => DateTimeOffset.UtcNow);
            Map(m => m.CreatedById).Constant(Guid.Parse("226b1dad-0065-44c6-acef-93186e7cd0f2"));
        }
    }
}
