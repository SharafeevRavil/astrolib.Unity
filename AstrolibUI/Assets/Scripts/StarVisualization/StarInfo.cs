using System;
using Helpers;
using StarVisualization.Stars;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace StarVisualization
{
    public class StarInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text hrText;
        
        [SerializeField] private TMP_Text distanceText;
        [SerializeField] private TMP_Text coordsText;
        
        [SerializeField] private TMP_Text bvText;
        [SerializeField] private TMP_Text vMagText;
        [SerializeField] private TMP_Text absMagText;
        
        [SerializeField] private TMP_Text specClassText;
        [SerializeField] private TMP_Text specTypeText;
        [SerializeField] private TMP_Text lumClassText;
        
        [SerializeField] private TMP_Text temperatureText;
        [SerializeField] private TMP_Text luminosityText;
        [SerializeField] private TMP_Text radiusText;
        
        [SerializeField] private TMP_Text rgbText;
        [SerializeField] private Image rgbImage;
        

        [SerializeField] private Button closeButton;
        [SerializeField] private Button setLeftStarComparisonButton;
        [SerializeField] private Button setRightStarComparisonButton;

        private Star _star;
        private Action _removeAction;

        public void Initialize(Star starData, Action removeAction)
        {
            hrText.text = $"Star: {starData.FullName}";
            
            distanceText.text = $"To Earth: {starData.DataCompilation.Distance} pc";
            distanceText.color = ColorHelper.OrangeCarrot;
            coordsText.text = $"RA = {CoordinateConverter.ConvertRa(starData.DataCompilation.Bsc5Star.Ra)}\n" +
                              $"Dec = {CoordinateConverter.ConvertDec(starData.DataCompilation.Bsc5Star.Dec)}";
            
            bvText.text = $"Bv = {starData.DataCompilation.Bsc5Star.Bv:F2}";
            bvText.color = ColorHelper.OrangeCarrot;
            vMagText.text = $"vMag = {starData.DataCompilation.Bsc5Star.VMag:F2}";
            vMagText.color = ColorHelper.OrangeCarrot;
            absMagText.text = $"absMag = {starData.AstrolibStar.AbsoluteMagnitude:F2}";
            absMagText.color = ColorHelper.LimeGreen;
            
            specClassText.text = $"{starData.DataCompilation.Bsc5Star.SpecType}:";
            specClassText.color = ColorHelper.OrangeCarrot;
            specTypeText.text = $"{starData.AstrolibStar.SpectralType}";
            specTypeText.color = ColorHelper.LimeGreen;
            lumClassText.text = $"{starData.AstrolibStar.LuminosityClass}";
            lumClassText.color = ColorHelper.LimeGreen;
            
            temperatureText.text = $"T = {starData.AstrolibStar.PhotosphereTemperature:F2}";
            temperatureText.color = ColorHelper.LimeGreen;
            luminosityText.text = $"L = {starData.AstrolibStar.Luminosity:F2} L⊙";
            luminosityText.color = ColorHelper.LimeGreen;
            radiusText.text = $"R = {starData.AstrolibStar.Radius:F2} R⊙";
            radiusText.color = ColorHelper.LimeGreen;

            rgbText.color = ColorHelper.LimeGreen;
            var color = starData.AstrolibStar.Rgb();
            rgbImage.color = new Color((float)color.r, (float)color.g, (float)color.b); 
            _star = starData;

            _removeAction = removeAction;
            closeButton.onClick.AddListener(Close);
            setLeftStarComparisonButton.onClick.AddListener(SetAsLeftStarInComparison);
            setRightStarComparisonButton.onClick.AddListener(SetAsRightStarInComparison);
        }

        public void Close()
        {
            _removeAction?.Invoke();
            Destroy(gameObject);
        }

        public void SetAsLeftStarInComparison()
        {
            var comparer = GameObject.Find("Comparison").GetComponent<StarComparison>();
            comparer.SetLeftStar(_star);
        }
        
        public void SetAsRightStarInComparison()
        {
            var comparer = GameObject.Find("Comparison").GetComponent<StarComparison>();
            comparer.SetRightStar(_star);
        }
    }
}