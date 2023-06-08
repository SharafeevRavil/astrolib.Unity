using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dataset
{
    public static class ConstellationReader
    {
        public static List<ConstellationDto> GetConstellations(string filename)
        {
            var textAsset = Resources.Load(filename) as TextAsset;
            Debug.Assert(textAsset != null, nameof(textAsset) + " != null");

            var lines = textAsset.text.Split('\n');
            return lines
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line =>
                {
                    var words = line.Split(",");
                    var shortName = words[0];
                    var enName = words[1];
                    var ruName = words[2];
                    var count = Convert.ToInt32(words[3]);
                    var list = new List<(int, int)>();
                    for (var i = 0; i < count; i++)
                    {
                        var s1 = words[4 + i * 2];
                        var s2 = words[4 + i * 2 + 1];
                        if (s1 == "NULL" || s2 == "NULL")
                            continue;
                        list.Add((Convert.ToInt32(s1), Convert.ToInt32(s2)));
                    }

                    return new ConstellationDto(shortName, enName, ruName, list);
                }).ToList();
        }
    }

    public class ConstellationDto
    {
        public string ShortName { get; set; }
        public string EnName { get; set; }
        public string RuName { get; set; }
        public List<(int, int)> StarsList { get; set; }

        public ConstellationDto(string shortName, string enName, string ruName, List<(int, int)> starsList)
        {
            ShortName = shortName;
            EnName = enName;
            RuName = ruName;
            StarsList = starsList;
        }
    }
}