using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dataset
{
    public static class BrightestCsvReader
    {
        public static Dictionary<int, double> GetStarDistance(string filename)
        {
            var textAsset = Resources.Load(filename) as TextAsset;
            Debug.Assert(textAsset != null, nameof(textAsset) + " != null");

            var lines = textAsset.text.Split("\n");
            return lines
                .Where(line => !string.IsNullOrEmpty(line))
                .SelectMany(line =>
                {
                    try
                    {
                        //find hr
                        var matches = Regex.Matches(line, @"HR (\d+)");
                        var match = matches.FirstOrDefault();
                        if (match == null)
                            throw new DataException("Corrupted data - no HR identifier");
                        var hrNumber = Convert.ToInt32(match.Groups[1].Value);
                        //get distance
                        var columns = line.Split(',');
                        var distance = Convert.ToDouble(columns[7].Replace(',', '.'), CultureInfo.InvariantCulture);
                        return new[] { (Key: hrNumber, Distance: distance) };
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning(e);
                        return Array.Empty<(int Key, double Distance)>();
                    }
                })
                .ToDictionary(star => star.Key, star => star.Distance);
        }
    }
}