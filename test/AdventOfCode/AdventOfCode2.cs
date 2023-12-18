using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using test.AdventOfCode.Data;

namespace test.AdventOfCode;

internal static class AdventOfCode2
{
    private static readonly List<Game> _games = ParseGames(Day2.Data);
    const int redCubes = 12, greenCubes = 13, blueCubes = 14;

    public static void DisplayData()
    {
        foreach (Game game in _games)
        {
            foreach (var round in game.Rounds)
            {
                Console.WriteLine($"{game.Id} ROUNDS: {round.Red} , {round.Green}, {round.Blue}");
            }
        }
    }
    public static int Part1Result() => _games.PossibleGamesId().Sum(game => game.Id);
    private static IEnumerable<Game> PossibleGamesId(this List<Game> games) => games.Where(game => game.Rounds
       .All(round => round.Red <= redCubes && round.Green <= greenCubes && round.Blue <= blueCubes));
       
    
    static List<Game> ParseGames(string input)
    {
        List<Game> games = new List<Game>();

        string[] gameStrings = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        //Console.WriteLine(gameStrings[0]);

        foreach (var gameString in gameStrings)
        {
            string[] roundStrings;
            int id = 0;
            var parts = gameString.Split(':', StringSplitOptions.RemoveEmptyEntries);
            //foreach (string s in parts) Console.WriteLine(s);
            if (parts.Length == 2)
            {
                id = int.Parse(parts[0].Trim().Split("Game").Last());
                roundStrings = parts[1].Split(';', StringSplitOptions.RemoveEmptyEntries);
                //foreach (string s in roundStrings) Console.WriteLine(s);
            }
            else throw new Exception("Wrong Parse");

            Game game = new Game { Rounds = new List<Round>(), Id = id };

            foreach (var roundString in roundStrings)
            {
                string[] numberAndColor = roundString.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                //foreach(string s in numberAndColor) Console.WriteLine(s);
                var red = 0;
                var green = 0;
                var blue = 0;
                foreach (var color in numberAndColor)
                {
                    string[] counts = color.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    //format {4,red}
                    //foreach(string s in counts) Console.WriteLine(s);
                    switch (counts[1])
                    {
                        case "red": red += int.Parse(counts[0]); break;
                        case "green": green += int.Parse(counts[0]); break;
                        case "blue": blue += int.Parse(counts[0]); break;
                        default: break;
                    }
                }
                game.Rounds.Add(new Round { Red = red, Green = green, Blue = blue });
            }

            games.Add(game);
        }

        return games;
    }

}
class Game
{
    public int Id { get; set; }
    public List<Round> Rounds { get; set; } = new();
}

class Round
{
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }
}
