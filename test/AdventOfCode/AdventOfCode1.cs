using System.Text.RegularExpressions;
using test.AdventOfCode.Data;

namespace test;

internal class AdventOfCode1
{
    private static readonly string[] _parsedData = ParsedData(Day1.Data);
    private static readonly string _day1Url = "https://adventofcode.com/2023/day/1";
    private static readonly Dictionary<string, string> _map = new()
    {
        {"one", "1"}, {"two", "2"}, {"three", "3"}, {"four", "4"}, {"five", "5"},
        {"six", "6"}, {"seven", "7"}, {"eight", "8"}, {"nine", "9"}
    };
    private static readonly string[] _arr = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    public AdventOfCode1()
    {

    }
    public void DisplayData()
    {
        foreach (var data in _parsedData) { Console.WriteLine(data); }
    }
    public int Count() => _parsedData.Length;
    public int GetPart1Result()
    {
        var list = new List<int>();
        foreach (var s in _parsedData)
        {
            var firstDigit = s.First(c => char.IsDigit(c));
            var lastDigit = s.Last(c => char.IsDigit(c));
            Console.WriteLine(firstDigit + "" + lastDigit);
            int parsed = int.Parse(new string(new char[] { firstDigit, lastDigit }));
            list.Add(parsed);
        }
        return list.Sum();
    }
    public int GetPart2ResultWithRegex()
    {
        string pattern = @"(?:one|two|three|four|five|six|seven|eight|nine|\d)";
        var regex = new Regex(pattern);
        var list = new List<int>();
        var test = "eighthree";
        var matches2 = regex.Matches(test);
        foreach (var item in matches2)
        {
            Console.WriteLine(item);
        }

        foreach (var s in _parsedData)
        {
            var matches = regex.Matches(s);
            if (matches.Count == 1)
            {
                var num = "";
                if (_map.TryGetValue(matches[0].Value, out string value)) num += value;
                else num += matches[0].Value;
                var toBeParsed = num + num;
                list.Add(int.Parse(toBeParsed));
            }
            else if (matches.Count >= 2)
            {
                string[] firstAndLast = { matches[0].Value, matches[^1].Value };
                var toBeParsed = "";
                foreach (var ss in firstAndLast)
                {
                    if (_map.TryGetValue(ss, out string value)) toBeParsed += value;
                    else toBeParsed += ss;
                }

                list.Add(int.Parse(toBeParsed));
            }
            else Console.WriteLine(s);
        }
        foreach (var item in list) Console.WriteLine($"{item}, {list.Count}");
        return list.Sum();

    } //pattern not matching edge cases like "eighthree"
    public int GetPart2Result()
    {
        var res = 0;
        foreach (var s in _parsedData)
        {
            res += GetFirst_And_Last_Numbers(s);
        }
        return res;
    }   //iterative slow solution
    private static string[]? ParsedData(string data)
    => data is not null ? data.Split("\r\n") : throw new ArgumentException();
    private int GetFirst_And_Last_Numbers(string s)
    {
        var list = new List<int>();
        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsDigit(s[i])) list.Add(s[i] - '0');
            else
            {
                foreach (string num in _arr)
                {
                    try
                    {
                        string match = s.Substring(i, num.Length);
                        if (match == num) list.Add(int.Parse(_map[match]));
                    }
                    catch
                    {

                    }
                }
            }
        }
        var res = int.Parse(string.Join("", new int[] { list[0], list[^1] }));
        Console.WriteLine(res);
        return res;
    }
}

