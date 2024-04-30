using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballClubManagerSeeder.Models;
using CsvHelper.Configuration;

namespace BasketballClubManagerSeeder.Mappers {
    public class PlayerExperienceMap: ClassMap<PlayerExperience> {
        PlayerExperienceMap() {
            Map(m => m.Id).Convert(args => {
                var idString = args.Row.GetField("id");
                return Guid.TryParse(idString, out Guid id) ? id : Guid.Empty;
            });
            Map(m => m.StartDate).Convert(args => DateOnly.FromDateTime(DateTime.Parse(args.Row.GetField("StartDate"))));
            Map(m => m.CreatedTime).Convert(args => DateTimeOffset.UtcNow);

            Map(m => m.CreatedById).Constant(Guid.Parse("226b1dad-0065-44c6-acef-93186e7cd0f2"));
            Map(m => m.TeamId).Convert(args => Guid.Parse(args.Row.GetField("TeamId"))); ;
            Map(m => m.PlayerId).Convert(args => Guid.Parse(args.Row.GetField("PlayerId"))); ;
        }
    }
}
