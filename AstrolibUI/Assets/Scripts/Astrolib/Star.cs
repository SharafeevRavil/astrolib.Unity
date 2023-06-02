using System;
using System.Text;

namespace Astrolib
{
    public class Star
    {
        private readonly IntPtr _instance;
        private const int StringMaxLenght = 1024;

        public double Mass
        {
            get => AstrolibNative.Star_GetMass(_instance);
            set => AstrolibNative.Star_SetMass(_instance, value);
        }
        
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
        
        public double Bv { get; set; }
        
        public double DistanceFromEarth { get; set; }
        
        public double AbsoluteMagnitude { get; set; }
        
        public double ApparentMagnitude { get; set; }
        
        public string SpectrumClass { get; set; }
        
        public Star()
        {
            _instance = AstrolibNative.Star_createInstance();
        }

        public Star(double mass, double radius, double photosphereTemperature, double bv, string spectrumClass)
        {
            _instance = AstrolibNative.Star_createInstance();
            AstrolibNative.Star_SetMass(_instance, mass);
            AstrolibNative.Star_SetRadius(_instance, radius);
            AstrolibNative.Star_SetPhotosphereTemperature(_instance, photosphereTemperature);
            Bv = bv;
            SpectrumClass = spectrumClass;
        }
        
        public Star(double mass, double radius, double photosphereTemperature, double bv, SpecType specType, LumClass lumClass)
        {
            _instance = AstrolibNative.Star_createInstance();
            AstrolibNative.Star_SetMass(_instance, mass);
            AstrolibNative.Star_SetRadius(_instance, radius);
            AstrolibNative.Star_SetPhotosphereTemperature(_instance, photosphereTemperature);
            Bv = bv;
            SpectrumClass = FormatSpectrum(specType, lumClass);
        }

        public double Luminosity => AstrolibNative.Star_Luminosity(_instance);

        public SpecType SpectralType => (SpecType)AstrolibNative.Star_spectralType(SpectrumClass);

        public LumClass LuminosityClass => (LumClass)AstrolibNative.Star_luminosityClass(SpectrumClass);

        public void InitMagnitudeFromAbsolute(double absMag, double dist)
        {
            AbsoluteMagnitude = absMag;
            DistanceFromEarth = dist;
            ApparentMagnitude = GetApparentMagnitude(AbsoluteMagnitude, DistanceFromEarth);
        }
        
        public void InitMagnitudeFromApparent(double appMag, double dist)
        {
            ApparentMagnitude = appMag;
            DistanceFromEarth = dist;
            AbsoluteMagnitude = GetAbsoluteMagnitude(ApparentMagnitude, DistanceFromEarth);
        }

        /// <summary>
        /// How much one star is brighter than the other
        /// </summary>
        /// <returns></returns>
        public double BrightnessRatio(Star otherStar) => 
            AstrolibNative.Star_brightnessRatio(otherStar.ApparentMagnitude - ApparentMagnitude);

        public double ColorTemperature => AstrolibNative.Star_colorTemperature(Bv, (int)LuminosityClass);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bv"></param>
        /// <remarks>Returns values as fractions for 0 to 1</remarks>
        /// <returns></returns>
        public (double r, double g, double b) Rgb()
        {
            AstrolibNative.Star_bmv2rgb(Bv, out var r, out var g, out var b);
            return (r, g, b);
        }
        
        public static double Bmv2Temp(double bv) => AstrolibNative.Star_bmv2temp(bv);

        public static double BolometricCorrection(double t) => AstrolibNative.Star_bolometricCorrection(t);

        public static double GetAbsoluteMagnitude(double appMag, double dist) =>
            AstrolibNative.Star_absoluteMagnitude(appMag, dist);

        public static double GetApparentMagnitude(double absMag, double dist) =>
            AstrolibNative.Star_apparentMagnitude(absMag, dist);
        
        public static double DistanceFromMagnitude(double appMag, double absMag) =>
            AstrolibNative.Star_distanceFromMagnitude(appMag, absMag);
        
        public static double GetMagnitudeDifference(double ratio) =>
            AstrolibNative.Star_magnitudeDifference(ratio);
        
        public static double MagnitudeSum(double mag1, double mag2) =>
            AstrolibNative.Star_magnitudeSum(mag1, mag2);
        
        public static double MoffatFunction(double max, double r2, double beta) =>
            AstrolibNative.Star_moffatFunction(max, r2, beta);
        
        public static double MoffatRadius(double z, double max, double beta) =>
            AstrolibNative.Star_moffatRadius(z, max, beta);

        public static string FormatSpectrum(SpecType specType, LumClass lumClass)
        {
            var str = new StringBuilder(StringMaxLenght);
            AstrolibNative.Star_formatSpectrum(str, StringMaxLenght, (int)specType, (int)lumClass);
            return str.ToString();
        }
        
        public static double GetLuminosity(double mv, double bc) =>
            AstrolibNative.Star_luminosity(mv, bc);
        
        public static double GetRadius(double lum, double temp) =>
            AstrolibNative.Star_luminosity(lum, temp);
    }
}