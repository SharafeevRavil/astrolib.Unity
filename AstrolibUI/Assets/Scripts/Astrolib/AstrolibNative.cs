using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Astrolib
{
    public static class AstrolibNative
    {
        private const string DLLName = "astrolib-my";

        [DllImport(DLLName)]
        public static extern void fibonacci_init(long a, long b);
    
        [DllImport(DLLName)]
        public static extern double CelsiusToKelvin(double celsiusTemperature);
    
        [DllImport(DLLName)]
        public static extern bool fibonacci_next();
    
        [DllImport(DLLName)]
        public static extern long fibonacci_current();
    
        [DllImport(DLLName)]
        public static extern int fibonacci_index();
    
    
        [DllImport(DLLName)]
        public static extern IntPtr Star_createInstance();
   
        [DllImport(DLLName)]
        public static extern void Star_deleteInstance(IntPtr instance);
        [DllImport(DLLName)]
        public static extern void Star_SetMass(IntPtr instance, double mass);
        [DllImport(DLLName)]
        public static extern double Star_GetMass(IntPtr instance);
        [DllImport(DLLName)]
        public static extern void Star_SetRadius(IntPtr instance, double radius);
        [DllImport(DLLName)]
        public static extern void Star_SetPhotosphereTemperature(IntPtr instance, double photosphereTemperature);
        [DllImport(DLLName)]
        public static extern double Star_GetRadius(IntPtr instance);
        [DllImport(DLLName)]
        public static extern double Star_GetPhotosphereTemperature(IntPtr instance);
        [DllImport(DLLName)]
        public static extern double Star_Luminosity(IntPtr instance);
        [DllImport(DLLName)]
        public static extern void Star_bmv2rgb(double bv, out double r, out double g, out double b);
        [DllImport(DLLName)]
        public static extern double Star_bmv2temp(double bv);
        [DllImport(DLLName)]
        public static extern double Star_colorTemperature(double bv, int lumClass);
        [DllImport(DLLName)]
        public static extern double Star_bolometricCorrection(double t);
        [DllImport(DLLName)]
        public static extern double Star_absoluteMagnitude(double appMag, double dist);
        [DllImport(DLLName)]
        public static extern double Star_apparentMagnitude(double absMag, double dist);
        [DllImport(DLLName)]
        public static extern double Star_distanceFromMagnitude(double appMag, double absMag);
        [DllImport(DLLName)]
        public static extern double Star_brightnessRatio(double magDiff);
        [DllImport(DLLName)]
        public static extern double Star_magnitudeDifference(double ratio);
        [DllImport(DLLName)]
        public static extern double Star_magnitudeSum(double mag1, double mag2);
        [DllImport(DLLName)]
        public static extern double Star_moffatFunction(double max, double r2, double beta);
        [DllImport(DLLName)]
        public static extern double Star_moffatRadius(double z, double max, double beta);
        [DllImport(DLLName)]
        public static extern int Star_spectralType(string spectrum);
        [DllImport(DLLName)]
        public static extern int Star_luminosityClass(string spectrum);
        [DllImport(DLLName)]
        public static extern bool Star_parseSpectrum(string spectrum, out int spectype, out int lumclass);
        [DllImport(DLLName)]
        public static extern void Star_formatSpectrum(StringBuilder str, int strlen, int spectype, int lumclass);
        [DllImport(DLLName)]
        public static extern double Star_luminosity(double mv, double bc);
        [DllImport(DLLName)]
        public static extern double Star_radius(double lum, double temp);
    }
}