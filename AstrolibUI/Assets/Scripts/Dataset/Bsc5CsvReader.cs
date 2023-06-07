using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Dataset
{
    public class Bsc5CsvReader
    {
        // data from https://heasarc.gsfc.nasa.gov/db-perl/W3Browse/w3table.pl?tablehead=name%3Dbsc5p&Action=More+Options
        public static List<Bsc5StarDto> ReadStarData(string filename)
        {
            var textAsset = Resources.Load(filename) as TextAsset;
            Debug.Assert(textAsset != null, nameof(textAsset) + " != null");

            var lines = textAsset.text.Split('\n');
            return lines
                .Skip(1)
                .Where(line => !string.IsNullOrEmpty(line))
                .SelectMany(line =>
                {
                    try
                    {
                        var columns = line.Split(';');
                        Debug.Assert(columns.Length == 7);

                        var hrNumber = Convert.ToInt32(columns[0][3..]); //remove "HR " text 
                        var altName = columns[1];
                        var ra = Convert.ToDouble(columns[2].Replace(',', '.'),
                            CultureInfo.InvariantCulture); // via ','
                        var dec = Convert.ToDouble(columns[3].Replace(',', '.'),
                            CultureInfo.InvariantCulture); // via ','
                        var vMag = Convert.ToDouble(columns[4].Replace(',', '.'),
                            CultureInfo.InvariantCulture); // via '.'
                        var bv = Convert.ToDouble(columns[5].Replace(',', '.'),
                            CultureInfo.InvariantCulture); // via ','
                        var specType = columns[6];
                        return new[] { new Bsc5StarDto(hrNumber, altName, ra, dec, vMag, bv, specType) };
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"[Bsc5Csv] Error parsing {line}");
                        Debug.LogWarning(e);
                        return Array.Empty<Bsc5StarDto>();
                    }
                })
                .ToList();
        }
    }
}