using System.Collections.Generic;
using System.Linq;
using Dataset;
using UnityEngine;
using UnityEngine.UI;

namespace StarVisualization.Stars
{
    public class ConstellationVisualizer : MonoBehaviour
    {
        [SerializeField] private string constellationFilename = "constellationship";
        [SerializeField] private Material material;
        [SerializeField] private float width = 0.2f;
        
        [SerializeField] private Button button;

        [SerializeField] private StarSky starSky;
        
        private List<ConstellationDto> _constellationData;
        
        private bool _generated;
        private bool _visible;
        private GameObject _constellationsObject;
        private double _lastToggledSec;

        private Image _buttonImage;
        private readonly Color _disabledColor = new(105/255f, 105/255f, 105/255f);
        private readonly Color _enabledColor = new(50/255f, 205/255f, 50/255f);

        private void Start()
        {
            _constellationData = ConstellationReader.GetConstellations(constellationFilename);
            
            button.onClick.AddListener(ToggleVisualization);
            _buttonImage = button.GetComponent<Image>();
            _buttonImage.color = _disabledColor;
        }

        public void ToggleVisualization()
        {
            if (!_generated)
            {
                GenerateLines(starSky.Stars, starSky.StarFieldScale);
                _generated = true;
            }

            _visible = !_visible;
            _constellationsObject.SetActive(_visible);
            _buttonImage.color = _visible ? _enabledColor : _disabledColor;
        }
        private void FixedUpdate()
        {
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
            foreach (var constellation in _constellationData)
            {
                // ReSharper disable once UseObjectOrCollectionInitializer
                var constellationObject = new GameObject($"Constellation {constellation.Name}");
                constellationObject.transform.parent = _constellationsObject.transform;
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
                    //lineRenderers.Add(lineRenderer);
                }
            }
        }
    }
}