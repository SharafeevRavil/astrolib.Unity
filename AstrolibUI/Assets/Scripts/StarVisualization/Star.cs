using System;
using Astrolib;
using Dataset;
using UnityEngine;

namespace StarVisualization
{
    public class Star
    {
        public Bsc5StarDto Bsc5Star { get; }
        public AstrolibStar AstrolibStar { get; }

        public Vector3 Position { get; set; }
        public float Size { get; set; }

        public Star(Bsc5StarDto bsc5Star, double dist)
        {
            Bsc5Star = bsc5Star;
            AstrolibStar = new AstrolibStar(Bsc5Star.Bv, bsc5Star.VMag, bsc5Star.SpecType, dist);

            CalculatePosition();
            CalculateSize();
        }

        private void CalculateSize() => Size = 1 - Mathf.InverseLerp(-1.46f, 7.96f, (float)Bsc5Star.VMag);

        public void CalculatePosition()
        {
            var raRad = Bsc5Star.Ra * Math.PI / 180d;
            var decRad = Bsc5Star.Dec * Math.PI / 180d;
            
            var x = Math.Cos(raRad);
            var y = Math.Sin(decRad);
            var z = Math.Sin(raRad);

            var yCos = Math.Cos(decRad);
            x *= yCos;
            z *= yCos;

            Position = new Vector3((float)x, (float)y, (float)z);
        }
    }
}