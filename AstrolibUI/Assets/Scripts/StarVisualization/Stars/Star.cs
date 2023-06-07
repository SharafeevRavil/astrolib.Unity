using System;
using Astrolib;
using Dataset;
using PlasticGui;
using UnityEngine;

namespace StarVisualization.Stars
{
    public class Star
    {
        public StarDataCompilation DataCompilation { get; }
        public AstrolibStar AstrolibStar { get; }

        public Vector3 Position { get; set; }
        public float Size { get; set; }
        
        public Star(StarDataCompilation dataCompilation)
        {
            DataCompilation = dataCompilation;
            AstrolibStar = new AstrolibStar(DataCompilation.Bsc5Star.Bv, dataCompilation.Bsc5Star.VMag, dataCompilation.Bsc5Star.SpecType, dataCompilation.Distance);

            CalculatePosition();
            CalculateSize();
        }

        private void CalculateSize() => Size = 1 - Mathf.InverseLerp(-1.46f, 7.96f, (float)DataCompilation.Bsc5Star.VMag);

        public void CalculatePosition()
        {
            var raRad = DataCompilation.Bsc5Star.Ra * Math.PI / 180d;
            var decRad = DataCompilation.Bsc5Star.Dec * Math.PI / 180d;
            
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