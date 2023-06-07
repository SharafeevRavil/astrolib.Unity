using StarVisualization.Stars;
using TMPro;
using UnityEngine;

namespace StarVisualization
{
    public class StarComparison : MonoBehaviour
    {
        private Star _starLeft;
        private Star _starRight;

        [SerializeField] private TMP_Text leftStarName;
        [SerializeField] private TMP_Text rightStarName;
        [SerializeField] private TMP_Text brightnessRatioText;

        public void SetLeftStar(Star star)
        {
            _starLeft = star;
            leftStarName.text = star.DataCompilation.Name ??  $"HR {star.DataCompilation.Bsc5Star.HrNumber}";
            if (_starRight != null)
                RecalculateRatio();
        }

        public void SetRightStar(Star star)
        {
            _starRight = star;
            rightStarName.text = star.DataCompilation.Name ??  $"HR {star.DataCompilation.Bsc5Star.HrNumber}";
            if (_starLeft != null)
                RecalculateRatio();
        }

        private void RecalculateRatio()
        {
            if (_starRight.AstrolibStar.ApparentMagnitude < _starLeft.AstrolibStar.ApparentMagnitude)
            {
                var brightnessRatio = _starRight.AstrolibStar.BrightnessRatio(_starLeft.AstrolibStar);
                brightnessRatioText.text = $"{rightStarName.text} is {brightnessRatio:F} times brighter than {leftStarName.text}";
            }
            else
            {
                var brightnessRatio = _starLeft.AstrolibStar.BrightnessRatio(_starRight.AstrolibStar);
                brightnessRatioText.text = $"{leftStarName.text} is {brightnessRatio:F} times brighter than {rightStarName.text}";
            }
        }
    }
}