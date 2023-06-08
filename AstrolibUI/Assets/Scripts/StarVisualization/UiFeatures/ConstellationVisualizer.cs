using System.Collections.Generic;
using System.Linq;
using Dataset;
using Helpers;
using StarVisualization.Stars;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StarVisualization.UiFeatures
{
    public class ConstellationVisualizer : MonoBehaviour
    {
        [SerializeField] private string constellationFilename = "constellationship";
        [SerializeField] private Material material;
        [SerializeField] private float width = 0.2f;
        
        [SerializeField] private Button button;
        [SerializeField] private StarSky starSky;
        [SerializeField] private InputFieldsHelper inputFieldsHelper;
        [SerializeField] private GameObject textPrefab;
        
        private List<ConstellationDto> _constellationData;
        
        private bool _generated;
        private int _visible; // 0 - not visible, 1 - const, 2 - with names
        private GameObject _constellationsObject;
        private GameObject _constellationsNamesObject;
        private double _lastToggledSec;

        private Image _buttonImage;

        private void Start()
        {
            _constellationData = ConstellationReader.GetConstellations(constellationFilename);
            
            button.onClick.AddListener(ToggleVisualization);
            _buttonImage = button.GetComponent<Image>();
            _buttonImage.color = ColorHelper.DisabledGray;
        }

        public void ToggleVisualization()
        {
            if (!_generated)
            {
                GenerateLines(starSky.Stars, starSky.StarFieldScale);
                _generated = true;
            }

            _visible++;
            _visible %= 3;
            _constellationsObject.SetActive(_visible != 0);
            _constellationsNamesObject.SetActive(_visible == 2);
            _buttonImage.color = _visible != 0 ? ColorHelper.LimeGreen : ColorHelper.DisabledGray;
        }
        private void FixedUpdate()
        {
            if (inputFieldsHelper.IsAnyFocused()) return;
            if (!Input.GetKey(KeyCode.Space)) return;
            
            var nowSec = Time.unscaledTime;
            if(nowSec - _lastToggledSec <= 0.5) return; //cooldown in seconds
            _lastToggledSec = nowSec;
            ToggleVisualization();
        }

        private void GenerateLines(IReadOnlyCollection<Star> stars, int starFieldScale)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            _constellationsObject = new GameObject($"Constellations");
            _constellationsObject.transform.parent = transform;
            // ReSharper disable once UseObjectOrCollectionInitializer
            _constellationsNamesObject = new GameObject($"Constellations names");
            _constellationsNamesObject.transform.parent = transform;
            foreach (var constellation in _constellationData)
            {
                // ReSharper disable once UseObjectOrCollectionInitializer
                var constellationObject = new GameObject($"Constellation [{constellation.ShortName}] {constellation.EnName} ({constellation.RuName})");
                constellationObject.transform.parent = _constellationsObject.transform;
                var posSum = new Vector3();
                for (var i = 0; i < constellation.StarsList.Count; i++)
                {
                    var (s1, s2) = constellation.StarsList[i];
                    var star1 = stars.FirstOrDefault(s => s.DataCompilation.Bsc5Star.HrNumber == s1);
                    if (star1 == null)
                    {
                        Debug.LogWarning($"Star HR {s1} was not found on the scene");
                        continue;
                    }

                    var star2 = stars.FirstOrDefault(s => s.DataCompilation.Bsc5Star.HrNumber == s2);
                    if (star2 == null)
                    {
                        Debug.LogWarning($"Star HR {s2} was not found on the scene");
                        continue;
                    }

                    // ReSharper disable once UseObjectOrCollectionInitializer
                    var lineObject = new GameObject($"Line {i}: HR {s1} -> {s2}");
                    lineObject.transform.parent = constellationObject.transform;
                    var lineRenderer = lineObject.AddComponent<LineRenderer>();

                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPositions(new[] { star1.Position * starFieldScale, star2.Position * starFieldScale });

                    lineRenderer.material = material;
                    lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, width);

                    posSum += (star1.Position + star2.Position) * starFieldScale / 2;
                }
                
                var text = Instantiate(textPrefab, posSum / constellation.StarsList.Count, Quaternion.identity, _constellationsNamesObject.transform);
                text.GetComponentInChildren<TMP_Text>().text = constellation.RuName;
            }
        }
    }
}