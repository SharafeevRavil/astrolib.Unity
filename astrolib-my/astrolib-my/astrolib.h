#pragma once

#ifdef ASTROLIB_EXPORTS
#define ASTROLIB_API __declspec(dllexport)
#else
#define ASTROLIB_API __declspec(dllimport)
#endif

#include "star.h"

extern "C" ASTROLIB_API double CelsiusToKelvin(double celsiusTemperature);
extern "C" ASTROLIB_API double KelvinToCelsius(double kelvinTemperature);

extern "C" ASTROLIB_API double Illumination(double lightFlow, double illuminatedSurfaceArea);

extern "C" ASTROLIB_API Star * Star_createInstance();
extern "C" ASTROLIB_API void Star_deleteInstance(Star * instance);
extern "C" ASTROLIB_API void Star_SetMass(Star * instance, double mass);
extern "C" ASTROLIB_API void Star_SetRadius(Star* instance, double raduus);
extern "C" ASTROLIB_API void Star_SetPhotosphereTemperature(Star* instance, double photosphereTemperature);
extern "C" ASTROLIB_API double Star_GetMass(Star * instance);
extern "C" ASTROLIB_API double Star_GetRadius(Star* instance);
extern "C" ASTROLIB_API double Star_GetPhotosphereTemperature(Star* instance);
extern "C" ASTROLIB_API double Star_Luminosity(Star* instance);
extern "C" ASTROLIB_API void Star_bmv2rgb(double bv, double& r, double& g, double& b);
extern "C" ASTROLIB_API double Star_bmv2temp(double bv);
extern "C" ASTROLIB_API double Star_colorTemperature(double bv, int lumClass);
extern "C" ASTROLIB_API double Star_bolometricCorrection(double t);
extern "C" ASTROLIB_API double Star_absoluteMagnitude(double appMag, double dist);
extern "C" ASTROLIB_API double Star_apparentMagnitude(double absMag, double dist);
extern "C" ASTROLIB_API double Star_distanceFromMagnitude(double appMag, double absMag);
extern "C" ASTROLIB_API double Star_brightnessRatio(double magDiff);
extern "C" ASTROLIB_API double Star_magnitudeDifference(double ratio);
extern "C" ASTROLIB_API double Star_magnitudeSum(double mag1, double mag2);
extern "C" ASTROLIB_API double Star_moffatFunction(double max, double r2, double beta);
extern "C" ASTROLIB_API double Star_moffatRadius(double z, double max, double beta);
extern "C" ASTROLIB_API int Star_spectralType(char* spectrum);
extern "C" ASTROLIB_API int Star_luminosityClass(char* spectrum);
extern "C" ASTROLIB_API bool Star_parseSpectrum(char* spectrum, int& spectype, int& lumclass);
extern "C" ASTROLIB_API void Star_formatSpectrum(char* str, int strlen, int spectype, int lumclass);
extern "C" ASTROLIB_API double Star_luminosity(double mv, double bc);
extern "C" ASTROLIB_API double Star_radius(double lum, double temp);