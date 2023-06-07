using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dataset
{
    public class StarReader : MonoBehaviour
    {
        [SerializeField] private string bsc5CsvFilename = "bsc5";
        [SerializeField] private string brightestCsvFilename = "brightest";
        [SerializeField] private string iauStarNamesFileName = "IAU star names";
        
        public List<StarDataCompilation> ReadStarData()
        {
            var stars = Bsc5CsvReader.ReadStarData(bsc5CsvFilename);
            var distanceDict = BrightestCsvReader.GetStarDistance(brightestCsvFilename);
            var namesDict = IAUStarNamesCsvReader.GetStarNames(iauStarNamesFileName);
            return stars
                .SelectMany(star =>
                {
                    var hrNumber = star.HrNumber;
                    if (!distanceDict.ContainsKey(hrNumber))
                    {
                        Debug.LogWarning($"Star HR {hrNumber}: distance was not found");
                        return Array.Empty<StarDataCompilation>();
                    }

                    var starName = namesDict.ContainsKey(hrNumber) ? namesDict[hrNumber] : ""; 
                    var dist = distanceDict[hrNumber];
                    return new[] { new StarDataCompilation(star, dist, starName) };
                })
                .ToList();
        }
    }

    public class StarDataCompilation
    {
        public Bsc5StarDto Star { get; }
        public double Distance { get; }
        
        public string Name { get; set; }

        public StarDataCompilation(Bsc5StarDto star, double distance, string name)
        {
            Star = star;
            Distance = distance;
            Name = name;
        }
    }
}