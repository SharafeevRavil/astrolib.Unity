using System;
using System.Text;

namespace Astrolib
{
    public class AstrolibStar
    {
        private readonly IntPtr _instance;
        private const int StringMaxLenght = 1024;
        
        public double Radius
        {
            get => AstrolibNative.Star_GetRadius(_instance);
            set => AstrolibNative.Star_SetRadius(_instance, value);
        }
        
        public double PhotosphereTemperature
        {
            get => AstrolibNative.Star_GetPhotosphereTemperature(_instance);
            set => AstrolibNative.Star_SetPhotosphereTemperature(_instance, value);
        }
        
        public double Bv { get; set; } // дано (бд1)
        public double DistanceFromEarth { get; set; } // дано (бд2)
        public double ApparentMagnitude { get; set; } // дано (бд1)
        public string SpectrumClass { get; set; } // дано (бд1)
        public double Luminosity { get; set; } // считаем
        public double AbsoluteMagnitude { get; set; } // считаем через видимую магнитуду и расстояние
        
        public AstrolibStar()
        {
            _instance = AstrolibNative.Star_createInstance();
        }

        public AstrolibStar(double bv, double apparentMagnitude, string spectrumClass, double distInParsecs) : this()
        {
            DistanceFromEarth = distInParsecs;
            Bv = bv;
            SpectrumClass = spectrumClass;
            PhotosphereTemperature = GetTempByBv(); //no temperature in dataset - calculate it from bv

            InitMagnitudeFromApparent(apparentMagnitude, distInParsecs);
            Luminosity = GetLuminosity(AbsoluteMagnitude, BolometricCorrection(PhotosphereTemperature));
            Radius = GetRadius(Luminosity, PhotosphereTemperature);
        }

        public SpecType SpectralType => (SpecType)AstrolibNative.Star_spectralType(SpectrumClass); // считаем из общей
        public LumClass LuminosityClass => (LumClass)AstrolibNative.Star_luminosityClass(SpectrumClass); // считаем из общей

        #region Magnitude
        // not used in UI
        public void InitMagnitudeFromAbsolute(double absMag, double dist)
        {
            AbsoluteMagnitude = absMag;
            DistanceFromEarth = dist;
            ApparentMagnitude = GetApparentMagnitude(AbsoluteMagnitude, DistanceFromEarth);
        }
        
        // used in UI
        public void InitMagnitudeFromApparent(double appMag, double dist)
        {
            ApparentMagnitude = appMag;
            DistanceFromEarth = dist;
            AbsoluteMagnitude = GetAbsoluteMagnitude(ApparentMagnitude, DistanceFromEarth);
        }
        
        // not used in UI
        public static double GetApparentMagnitude(double absMag, double dist) =>
            AstrolibNative.Star_apparentMagnitude(absMag, dist);
        
        // used in UI
        public static double GetAbsoluteMagnitude(double appMag, double dist) =>
            AstrolibNative.Star_absoluteMagnitude(appMag, dist);
        #endregion

        /// <summary>
        /// How much one star is brighter than the other
        /// </summary>
        /// <returns></returns>
        public double BrightnessRatio(AstrolibStar otherAstrolibStar) => 
            AstrolibNative.Star_brightnessRatio(otherAstrolibStar.ApparentMagnitude - ApparentMagnitude);


        #region Temperature
        public double GetTempByBvLumClass => AstrolibNative.Star_colorTemperature(Bv, (int)LuminosityClass);
        public double GetTempByBv() => Bmv2Temp(Bv);
        #endregion
        /// <summary>
        /// Get RGB by Bv
        /// </summary>
        /// <remarks>Returns values as fractions for 0 to 1</remarks>
        /// <returns></returns>
        public (double r, double g, double b) Rgb()
        {
            AstrolibNative.Star_bmv2rgb(Bv, out var r, out var g, out var b);
            return (r, g, b);
        }
        

        // Calculates bc used for luminosity calculation
        public static double BolometricCorrection(double t) => AstrolibNative.Star_bolometricCorrection(t);


        #region MyRegion
        // maybe will be used in UI
        public static double MoffatFunction(double max, double r2, double beta) =>
            AstrolibNative.Star_moffatFunction(max, r2, beta);
        // maybe will be used in UI
        public static double MoffatRadius(double z, double max, double beta) =>
            AstrolibNative.Star_moffatRadius(z, max, beta);
        #endregion
        
        #region StaticMethods
        // not used in UI
        public static double DistanceFromMagnitude(double appMag, double absMag) =>
            AstrolibNative.Star_distanceFromMagnitude(appMag, absMag);
        
        // not used in UI
        public static double GetMagnitudeDifference(double ratio) =>
            AstrolibNative.Star_magnitudeDifference(ratio);
        
        // not be used in UI
        public static double MagnitudeSum(double mag1, double mag2) =>
            AstrolibNative.Star_magnitudeSum(mag1, mag2);
        
        // not used in UI
        public static string FormatSpectrum(SpecType specType, LumClass lumClass)
        {
            var str = new StringBuilder(StringMaxLenght);
            AstrolibNative.Star_formatSpectrum(str, StringMaxLenght, (int)specType, (int)lumClass);
            return str.ToString();
        }
        // used in UI
        public static double Bmv2Temp(double bv) => AstrolibNative.Star_bmv2temp(bv);
        
        // used in UI
        public static double GetLuminosity(double mv, double bc) =>
            AstrolibNative.Star_luminosity(mv, bc);
        // used in UI
        public static double GetRadius(double lum, double temp) =>
            AstrolibNative.Star_radius(lum, temp);
        #endregion
    }
}