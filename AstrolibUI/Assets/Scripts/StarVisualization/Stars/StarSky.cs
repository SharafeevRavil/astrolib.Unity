﻿using System.Collections.Generic;
using System.Linq;
using Dataset;
using UnityEngine;
using UnityEngine.Serialization;

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
        public IReadOnlyList<GameObject> StarObjects { get; private set; }

        private static readonly int Size = Shader.PropertyToID("_Size");

        private GameObject _starsParentGo;
        
        private void Start()
        {
            Stars = starReader.ReadStarData()
                .Select(compilation => new Star(compilation))
                .ToList();

            _starsParentGo = new GameObject("Stars");
            _starsParentGo.transform.parent = transform;
            
            StarObjects = Stars
                .Select(star =>
                {
                    var starGo = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    var thisTransform = transform;
                    starGo.transform.parent = _starsParentGo.transform;
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
            if (StarObjects == null) return;
            for (var i = 0; i < StarObjects.Count; i++)
            {
                // Update the size set in the shader.
                var material = StarObjects[i].GetComponent<MeshRenderer>().material;
                material.SetFloat(Size, Mathf.Lerp(starSizeMin, starSizeMax, Stars[i].Size));
            }
        }
    }
}