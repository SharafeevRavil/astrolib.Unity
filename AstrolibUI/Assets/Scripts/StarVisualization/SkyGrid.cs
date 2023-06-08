﻿using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace StarVisualization
{
    public class SkyGrid : MonoBehaviour
    {
        [SerializeField] private int meridianCount = 24; // Количество меридианов
        [SerializeField] private int parallelCount = 24; // Количество параллелей
        [SerializeField] private float radius = 400f; // Радиус сетки

        [SerializeField] private Button button;
        
        [SerializeField] private Material material;
        [SerializeField] private float width = 0.5f;
            
        private GameObject _meridiansGo;
        private GameObject _parallelsGo;
        
        private bool _generated;
        private bool _visible;
        private Image _buttonImage;
        
        private void Start()
        {
            button.onClick.AddListener(ToggleGrid);
            _buttonImage = button.GetComponent<Image>();
            _buttonImage.color = ColorHelper.DisabledGray;
        }

        private void ToggleGrid()
        {
            if (!_generated)
            {
                GenerateGrid();
                _generated = true;
            }

            _visible = !_visible;
            _meridiansGo.SetActive(_visible);
            _parallelsGo.SetActive(_visible);
            _buttonImage.color = _visible ? ColorHelper.LimeGreen : ColorHelper.DisabledGray;
        }

        private void GenerateGrid()
        {
            var thisTransform = transform;
            _meridiansGo = new GameObject("Meridians");
            _meridiansGo.transform.parent = thisTransform;
            _parallelsGo = new GameObject("Parallels");
            _parallelsGo.transform.parent = thisTransform;

            var parallelInterval = 360f / parallelCount;

            GenerateMeridians(parallelInterval);
            GenerateParallels(parallelInterval);
        }

        private void GenerateMeridians(float parallelInterval)
        {
            for (var i = 0; i < meridianCount; i++)
            {
                var angle = i * 360f / meridianCount; // Угол меридиана
                var points = new Vector3[parallelCount / 2 + 1];

                // Создаем точки меридиана
                for (var j = 0; j <= parallelCount / 2; j++)
                {
                    var latitude = j * parallelInterval; // Широта точки
                    var x = Mathf.Sin(latitude * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                    var y = Mathf.Cos(latitude * Mathf.Deg2Rad) * radius;
                    var z = Mathf.Sin(latitude * Mathf.Deg2Rad) * Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                    points[j] = new Vector3(x, y, z);
                }

                // Создаем LineRenderer и добавляем точки
                var lineObject = new GameObject($"Meridian {i}");
                lineObject.transform.parent = _meridiansGo.transform;
                var lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = parallelCount / 2 + 1;
                lineRenderer.SetPositions(points);

                lineRenderer.material = material;
                lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, width);
                // Дополнительные настройки LineRenderer
            }
        }

        private void GenerateParallels(float parallelInterval)
        {
            for (var i = 1; i < parallelCount; i++)
            {
                var latitude = i * parallelInterval; // Широта параллели
                var points = new Vector3[meridianCount];

                // Создаем точки параллели
                for (var j = 0; j < meridianCount; j++)
                {
                    var angle = j * 360f / meridianCount; // Угол меридиана
                    var x = Mathf.Sin(latitude * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
                    var y = Mathf.Cos(latitude * Mathf.Deg2Rad) * radius;
                    var z = Mathf.Sin(latitude * Mathf.Deg2Rad) * Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
                    points[j] = new Vector3(x, y, z);
                }

                // Создаем LineRenderer и добавляем точки
                var lineObject = new GameObject($"Parallel {i}");
                lineObject.transform.parent = _parallelsGo.transform;
                var lineRenderer = lineObject.AddComponent<LineRenderer>();
                lineRenderer.positionCount = meridianCount;
                lineRenderer.SetPositions(points);

                lineRenderer.material = material;
                lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, width);
                // Дополнительные настройки LineRenderer
            }
        }
    }
}