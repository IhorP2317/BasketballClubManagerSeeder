using BasketballClubManagerSeeder.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballClubManagerSeeder.Mappers {
    public class PlayerMap : ClassMap<Player> {
        public PlayerMap() {
            Map(m => m.Id).Convert(args => {
                var idString = args.Row.GetField("Id");
                return Guid.TryParse(idString, out Guid id) ? id : Guid.Empty; 
            });
            Map(m => m.LastName).Name("LastName");
            Map(m => m.FirstName).Name("FirstName");

            Map(m => m.DateOfBirth).Convert(args => DateOnly.FromDateTime(DateTime.Parse(args.Row.GetField("DateOfBirth"))));

            Map(m => m.Country).Name("Country");
            Map(m => m.Height).Name("Height");
            Map(m => m.Weight).Name("Weight");
            Map(m => m.Position).Name("Position");
            Map(m => m.JerseyNumber).Name("JerseyNumber");


            Map(m => m.CreatedTime).Convert(args => DateTimeOffset.UtcNow);


            Map(m => m.CreatedById).Constant(Guid.Parse("226b1dad-0065-44c6-acef-93186e7cd0f2"));

            Map(m => m.TeamId).Convert(args => Guid.Parse(args.Row.GetField("TeamId"))); ;
        }



    }



}
