using System.Collections.Generic;
using System.Linq;
using Dataset;
using UnityEngine;

namespace StarVisualization.Stars
{
    public class StarSky : MonoBehaviour
    {
        [Range(0, 100)] [SerializeField] private float starSizeMin;
        [Range(0, 100)] [SerializeField] private float starSizeMax;
        
        [Range(0, 500)] [SerializeField] private int starFieldScale = 400;
        public int StarFieldScale => starFieldScale;

        [SerializeField] private StarReader starReader;

        public List<Star> Stars { get; private set; }
        private List<GameObject> _starObjects;

        private static readonly int Size = Shader.PropertyToID("_Size");

        private void Start()
        {
            Stars = starReader.ReadStarData()
                .Select(compilation => new Star(compilation))
                .ToList();
            
            _starObjects = Stars
                .Select(star =>
                {
                    var starGo = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    var thisTransform = transform;
                    starGo.transform.parent = thisTransform;
                    starGo.name = $"HR {star.DataCompilation.Bsc5Star.HrNumber}";
                    starGo.transform.localPosition = star.Position * StarFieldScale;
                    starGo.transform.LookAt(thisTransform.position);
                    starGo.transform.Rotate(0, 180, 0);

                    var dataHolder = starGo.AddComponent<StarDataHolder>();
                    dataHolder.Star = star;
                    
                    var material = starGo.GetComponent<MeshRenderer>().material;
                    material.shader = Shader.Find("Unlit/StarShader");
                    material.SetFloat(Size, Mathf.Lerp(starSizeMin, starSizeMax, star.Size));
                    var rgb = star.AstrolibStar.Rgb();
                    material.color = new Color((float)rgb.r, (float)rgb.g, (float)rgb.b);
                    return starGo;
                })
                .ToList();
        }

        private void OnValidate()
        {
            if (_starObjects == null) return;
            for (var i = 0; i < _starObjects.Count; i++)
            {
                // Update the size set in the shader.
                var material = _starObjects[i].GetComponent<MeshRenderer>().material;
                material.SetFloat(Size, Mathf.Lerp(starSizeMin, starSizeMax, Stars[i].Size));
            }
        }
    }
}