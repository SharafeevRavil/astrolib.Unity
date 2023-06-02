using System.Text;
using Astrolib;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("before");
        AstrolibNative.fibonacci_init(16, 28);
        Debug.Log("after");
        Debug.Log(AstrolibNative.fibonacci_next());
        Debug.Log(AstrolibNative.fibonacci_next());
        Debug.Log(AstrolibNative.fibonacci_next());
        Debug.Log(AstrolibNative.fibonacci_current());
        Debug.Log(AstrolibNative.fibonacci_index());
        
        Debug.Log(AstrolibNative.CelsiusToKelvin(100));

        var star = AstrolibNative.Star_createInstance();
        Debug.Log("created");
        Debug.Log(star);
        //AstrolibNative.Star_SetMass(star, 5);
        Debug.Log("set mass");
        Debug.Log(AstrolibNative.Star_GetMass(star));
        AstrolibNative.Star_deleteInstance(star);
        Debug.Log("instance deleted");
        
        AstrolibNative.Star_SetRadius(star, 5);
        AstrolibNative.Star_SetPhotosphereTemperature(star, 5);
        Debug.Log(AstrolibNative.Star_Luminosity(star));
        AstrolibNative.Star_bmv2rgb(1000, out var r, out var g, out var b);
        Debug.Log("bmv2rgb");
        Debug.Log(b);
        
        var str = new StringBuilder(100);
        AstrolibNative.Star_formatSpectrum(str, 100, 1, 1);

        Debug.Log(str.ToString());
        
        Debug.Log(AstrolibNative.Star_spectralType("O"));
        Debug.Log(AstrolibNative.Star_luminosityClass("O"));
        Debug.Log(AstrolibNative.Star_parseSpectrum("O", out var spectype, out var lumclass));
        Debug.Log(spectype);
        Debug.Log(lumclass);
    }
}
