using Helpers;
using StarVisualization.Stars;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace StarVisualization.UiFeatures
{
    public class StarComparison : MonoBehaviour
    {
        private Star _starLeft;

        public Star StarLeft
        {
            get => _starLeft;
            set
            {
                EnableIfNot();
                
                _starLeft = value;
                if (_starLeft == null)
                {
                    leftStarName.text = "";
                    brightnessRatioText.text = "";
                    removeLeft.gameObject.SetActive(false);
                }
                else
                {
                    leftStarName.text = _starLeft.DataCompilation.Name ??
                                        $"HR {_starLeft.DataCompilation.Bsc5Star.HrNumber}";
                    if (StarRight != null)
                        RecalculateRatio();
                    removeLeft.gameObject.SetActive(true);
                }
            }
        }

        private Star _starRight;

        public Star StarRight
        {
            get => _starRight;
            set
            {
                EnableIfNot();
                
                _starRight = value;
                if (_starRight == null)
                {
                    rightStarName.text = "";
                    brightnessRatioText.text = "";
                    removeRight.gameObject.SetActive(false);
                }
                else
                {
                    rightStarName.text = _starRight.DataCompilation.Name ??
                                         $"HR {_starRight.DataCompilation.Bsc5Star.HrNumber}";
                    if (StarLeft != null)
                        RecalculateRatio();
                    removeRight.gameObject.SetActive(true);
                }
            }
        }

        [SerializeField] private RectTransform panel;
        [SerializeField] private TMP_Text leftStarName;
        [SerializeField] private TMP_Text rightStarName;
        [SerializeField] private TMP_Text brightnessRatioText;

        [SerializeField] private Button removeLeft;
        [SerializeField] private Button removeRight;
        [SerializeField] private Button toggleButton;
        [SerializeField] private Button closeButton;

        private bool _visible;

        private void Start()
        {
            toggleButton.GetComponent<Image>().color = ColorHelper.DisabledGray;
            
            toggleButton.onClick.AddListener(TogglePanel);
            closeButton.onClick.AddListener(TogglePanel);
            removeLeft.onClick.AddListener(() => StarLeft = null);
            removeRight.onClick.AddListener(() => StarRight = null);
        }

        public void EnableIfNot()
        {
            if (!_visible) TogglePanel();
        }

        public void TogglePanel()
        {
            _visible = !_visible;
            panel.gameObject.SetActive(_visible);
            toggleButton.gameObject.SetActive(!_visible);
        }

        private void RecalculateRatio()
        {
            var (more, less, moreText, lessText) =
                StarRight.AstrolibStar.ApparentMagnitude < StarLeft.AstrolibStar.ApparentMagnitude
                    ? (StarRight, StarLeft, rightStarName.text, leftStarName.text)
                    : (StarLeft, StarRight, leftStarName.text, rightStarName.text);

            var brightnessRatio = more.AstrolibStar.BrightnessRatio(less.AstrolibStar);
            brightnessRatioText.text = $"{moreText} is {brightnessRatio:F} times brighter than {lessText}";
        }
    }
}