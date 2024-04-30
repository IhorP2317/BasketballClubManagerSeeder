using BasketballClubManagerSeeder.Data;
using BasketballClubManagerSeeder.Models;
using CsvHelper;
using System.Globalization;

string teamFilePath = @"D:\Studying\DB\Course Work\BasketballClubManagerSeeder\BasketballClubManagerSeeder\csv\teams.csv";
string playerFilePath = @"D:\Studying\DB\Course Work\BasketballClubManagerSeeder\BasketballClubManagerSeeder\csv\players.csv";
string coachFilePath = @"D:\Studying\DB\Course Work\BasketballClubManagerSeeder\BasketballClubManagerSeeder\csv\coaches.csv";
string playerExperienceFilePath = @"D:\Studying\DB\Course Work\BasketballClubManagerSeeder\BasketballClubManagerSeeder\csv\PlayerExperiences.csv";
string matchesFilePath = @"D:\Studying\DB\Course Work\BasketballClubManagerSeeder\BasketballClubManagerSeeder\csv\Matches.csv";



DataImporter importer = new DataImporter(new ExoftBasketballClubContext());
importer.CheckTeamIds(teamFilePath, coachFilePath);
//await importer.ImportData(teamFilePath, playerFilePath, coachFilePath, playerExperienceFilePath, matchesFilePath);
await importer.GenerateStatisticsForMatches();



