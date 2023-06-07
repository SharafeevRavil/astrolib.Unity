using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dataset
{
    public static class IauStarNamesCsvReader
    {
        public static Dictionary<int, string> GetStarNames(string filename)
        {
            var textAsset = Resources.Load(filename) as TextAsset;
            Debug.Assert(textAsset != null, nameof(textAsset) + " != null");

            var lines = textAsset.text.Split("\n");
            return lines
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line =>
                {
                    var split = line.Split(';');
                    return (Name: split[0], Key: split[1]);
                })
                .Where(star => Regex.IsMatch(star.Key,  @"HR (\d+)"))
                .ToDictionary(star => Convert.ToInt32(star.Key[3..]), star => star.Name);
        }
    }
}