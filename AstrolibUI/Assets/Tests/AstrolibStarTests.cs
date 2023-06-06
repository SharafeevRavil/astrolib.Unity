using System.Collections;
using System.Collections.Generic;
using Astrolib;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class AstrolibStarTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Star_Radius_Get()
    {
        var star = new AstrolibStar();
            
        star.Radius = 5;
        Assert.AreEqual(5, star.Radius);
    }
    
    /*[Test]
    public void Star_Mass_Get()
    {
        var star = new AstrolibStar();
            
        star.Mass = 5;
        Assert.AreEqual(5, star.Mass);
    }*/
    
    [Test]
    public void Star_PhotosphereTemperature_Get()
    {
        var star = new AstrolibStar();
            
        star.PhotosphereTemperature = 5;
        Assert.AreEqual(5, star.PhotosphereTemperature);
    }
    
    [Test]
    public void Star_Luminosity_Works()
    {
        //var star = new AstrolibStar(5, 5, 5, 5, SpecType.A0, LumClass.Ia);
        var star = new AstrolibStar
        {
            Radius = 5,
            PhotosphereTemperature = 5
        };
        Debug.Log(star.Luminosity);
        Assert.AreEqual(0, star.Luminosity, 0.1);
    }
    
    [Test]
    public void Star_Bmv2Rgb_Works()
    {
        var (r, g, b) = new AstrolibStar{Bv = 5}.Rgb();
        Debug.Log(r);
        Debug.Log(g);
        Debug.Log(b);
        Assert.AreEqual(1, r);
        Assert.AreEqual(0, g);
        Assert.AreEqual(0, b);
    }
    
    [Test]
    public void Star_Bmv2Temp_Works()
    {
        var temp = AstrolibStar.Bmv2Temp(5);
        Debug.Log(temp);
        Assert.AreEqual(1610, temp, 10);
    }
    
    [Test]
    public void Star_ColorTemperature_Works()
    {
        var temp = new AstrolibStar{Bv = 5, SpectrumClass = "O0Ia"}.GetTempByBvLumClass;
        Debug.Log(temp);
        Assert.AreEqual(0, temp, 0.1);
    }
    
    [Test]
    public void Star_BolometricCorrection_Works()
    {
        var result = AstrolibStar.BolometricCorrection(5);
        Debug.Log(result);
        Assert.AreEqual(-10100, result, 100);
    }
    
    [Test]
    public void Star_AbsoluteMagnitude_Works()
    {
        var result = AstrolibStar.GetAbsoluteMagnitude(5, 5);
        Debug.Log(result);
        Assert.AreEqual(6.5, result, 0.1);
    }
    
    [Test]
    public void Star_ApparentMagnitude_Works()
    {
        var result = AstrolibStar.GetApparentMagnitude(5, 5);
        Debug.Log(result);
        Assert.AreEqual(3.5, result, 0.1);
    }
    
    [Test]
    public void Star_DistanceFromMagnitude_Works()
    {
        var result = AstrolibStar.DistanceFromMagnitude(5, 5);
        Debug.Log(result);
        Assert.AreEqual(10, result);
    }
    
    [Test]
    public void Star_BrightnessRatio_Works()
    {
        var sun = new AstrolibStar();
        sun.InitMagnitudeFromApparent(-26.832, 0);
        var moon = new AstrolibStar();
        moon.InitMagnitudeFromApparent(-12.74, 0);
        var result = sun.BrightnessRatio(moon);
        Debug.Log(result);
        Assert.AreEqual(430000, result, 10000);
    }
    
    [Test]
    public void Star_MagnitudeDifference_Works()
    {
        var result = AstrolibStar.GetMagnitudeDifference(5);
        Debug.Log(result);
        Assert.AreEqual(-1.7, result, 0.1);
    }
    
    [Test]
    public void Star_MagnitudeSum_Works()
    {
        var result = AstrolibStar.MagnitudeSum(5, 5);
        Debug.Log(result);
        Assert.AreEqual(4.2, result, 0.1);
    }
    
    [Test]
    public void Star_MoffatFunction_Works()
    {
        var result = AstrolibStar.MoffatFunction(5, 5, 5);
        Debug.Log(result);
        Assert.AreEqual(0, result, 0.1);
    }
    
    [Test]
    public void Star_MoffatRadius_Works()
    {
        var result = AstrolibStar.MoffatRadius(5, 5, 5);
        Debug.Log(result);
        Assert.AreEqual(0, result, 0.1);
    }
    
    [Test]
    public void Star_SpectralType_Works()
    {
        var result = new AstrolibStar{SpectrumClass = "O0Ia0"}.SpectralType;
        Debug.Log(result);
        Assert.AreEqual(SpecType.O0, result);
    }
    
    [Test]
    public void Star_LuminosityClass_Works()
    {
        var result = new AstrolibStar{SpectrumClass = "O0Ia0"}.LuminosityClass;
        Debug.Log(result);
        Assert.AreEqual(LumClass.Ia0, result);
    }

    [Test]
    public void Star_FormatSpectrum_Works()
    {
        var result = AstrolibStar.FormatSpectrum(SpecType.A0, LumClass.Ia);
        Debug.Log(result);
        Assert.AreEqual("A0Ia", result);
    }
    
    [Test]
    public void Star_StaticLuminosity_Works()
    {
        var result = AstrolibStar.GetLuminosity(5, 5);
        Debug.Log(result);
        Assert.AreEqual(0, result, 0.1);
    }
    
    [Test]
    public void Star_StarRadius_Works()
    {
        var result = AstrolibStar.GetRadius(126000, 3600);
        Debug.Log(result);
        Assert.AreEqual(900, result, 300);
    }

    [Test]
    public void Star_Betelgeuse()
    {
        var betelgeuse = new AstrolibStar(1.85, 1, "M1Ia", 168.1);
        /*{
            //Mass = 1.9885e30,
            Radius = 764,
            PhotosphereTemperature = 3600,
            Bv = 1.85,
            SpectrumClass = "G2V",
            ApparentMagnitude = -5.85
        };*/


        var (r, g, b) = betelgeuse.Rgb();
        Debug.Log($"Radius: {betelgeuse.Radius}");
        Debug.Log($"Absolute magnitude: {betelgeuse.AbsoluteMagnitude}");
        Debug.Log($"Luminosity: {betelgeuse.Luminosity}");
        Debug.Log($"Temp: {betelgeuse.PhotosphereTemperature}");
        Debug.Log($"SpecType: {betelgeuse.SpectralType}");
        Debug.Log($"LumClass: {betelgeuse.LuminosityClass}");
        Debug.Log($"Sun rgb colour: ({r}, {g}, {b}");
        Assert.AreEqual(130000, betelgeuse.Luminosity, 50000);
        Assert.AreEqual(3600, betelgeuse.PhotosphereTemperature, 300);
        Assert.AreEqual(900, betelgeuse.Radius, 300);
        Assert.AreEqual(betelgeuse.SpectrumClass, AstrolibStar.FormatSpectrum(betelgeuse.SpectralType, betelgeuse.LuminosityClass));
    }
}
