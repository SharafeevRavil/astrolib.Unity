#include "pch.h"

#include "astrolib.h"
#include "constants.h"

using namespace std;

double CelsiusToKelvin(const double celsiusTemperature)
{
    return celsiusTemperature - ABSOLUTE_ZERO_CELSIUS;
}

double KelvinToCelsius(const double kelvinTemperature)
{
    return kelvinTemperature + ABSOLUTE_ZERO_CELSIUS;
}

double Illumination(const double lightFlow, const double illuminatedSurfaceArea)
{
    return lightFlow / illuminatedSurfaceArea;
}

Star* Star_createInstance()
{
    return new Star();
}

void Star_deleteInstance(Star* instance)
{
    delete instance;
}

void Star_SetMass(Star* instance, double mass)
{
    instance->SetMass(mass);
}

void Star_SetRadius(Star* instance, double radius)
{
    instance->SetRadius(radius);
}

void Star_SetPhotosphereTemperature(Star* instance, double photosphereTemperature)
{
    instance->SetPhotosphereTemperature(photosphereTemperature);
}

double Star_GetMass(Star* instance)
{
    return instance->GetMass();
}

double Star_GetRadius(Star* instance) 
{
    return instance->GetRadius();
}

double Star_GetPhotosphereTemperature(Star* instance) 
{
    return instance->GetPhotosphereTemperature();
}

double Star_Luminosity(Star* instance)
{
    return instance->Luminosity();
}

void Star_bmv2rgb(double bv, double& r, double& g, double& b)
{
    return Star::bmv2rgb(bv, r, g, b);
}

double Star_bmv2temp(double bv)
{
    return Star::bmv2temp(bv);
}

double Star_colorTemperature(double bv, int lumClass)
{
    return Star::colorTemperature(bv, lumClass);
}

double Star_bolometricCorrection(double t)
{
    return Star::bolometricCorrection(t);
}

double Star_absoluteMagnitude(double appMag, double dist)
{
    return Star::absoluteMagnitude(appMag, dist);
}

double Star_apparentMagnitude(double absMag, double dist)
{
    return Star::apparentMagnitude(absMag, dist);
}

double Star_distanceFromMagnitude(double appMag, double absMag)
{
    return Star::distanceFromMagnitude(appMag, absMag);
}

double Star_brightnessRatio(double magDiff)
{
    return Star::brightnessRatio(magDiff);
}

double Star_magnitudeDifference(double ratio)
{
    return Star::magnitudeDifference(ratio);
}

double Star_magnitudeSum(double mag1, double mag2)
{
    return Star::magnitudeSum(mag1, mag2);
}

double Star_moffatFunction(double max, double r2, double beta)
{
    return Star::moffatFunction(max, r2, beta);
}

double Star_moffatRadius(double z, double max, double beta)
{
    return Star::moffatRadius(z, max, beta);
}

int Star_spectralType(char* spectrum)
{
    std::string str(spectrum);
    return Star::spectralType(str);
}

int Star_luminosityClass(char* spectrum)
{
    std::string str(spectrum);
    return Star::luminosityClass(str);
}

bool Star_parseSpectrum(char* spectrum, int& spectype, int& lumclass)
{
    std::string str(spectrum);
    return Star::parseSpectrum(spectrum, spectype, lumclass);
}

void Star_formatSpectrum(char* str, int strlen, int spectype, int lumclass)
{
    std::string result = Star::formatSpectrum(spectype, lumclass);

    result = result.substr(0, strlen);

    std::copy(result.begin(), result.end(), str);
    str[min(strlen - 1, (int)result.size())] = 0;
}

double Star_luminosity(double mv, double bc)
{
    return Star::luminosity(mv, bc);
}

double Star_radius(double lum, double temp)
{
    return Star::radius(lum, temp);
}