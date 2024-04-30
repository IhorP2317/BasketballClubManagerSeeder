using BasketballClubManagerSeeder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace BasketballClubManagerSeeder.Mappers {
    public class TeamMap : ClassMap<Team> {
        public TeamMap() {
            Map(m => m.Id).Convert(args => Guid.Parse(args.Row.GetField("id")));
            Map(m => m.Name).Name("name");
            Map(m => m.CreatedTime).Convert(args => DateTimeOffset.UtcNow);
            Map(m => m.CreatedById).Constant(Guid.Parse("226b1dad-0065-44c6-acef-93186e7cd0f2"));  
        }
    }
}
