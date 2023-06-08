using System.Collections.Generic;
using StarVisualization.Stars;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StarVisualization.UiFeatures.StarInfo
{
    public class StarInfoOpen : MonoBehaviour
    {
        [SerializeField] private Camera castCamera;
        [SerializeField] private float sphereCastRadius = 0.1f;

        [SerializeField] private GameObject panelPrefab;

        private HashSet<int> _openedStars;

        private void Start()
        {
            _openedStars = new HashSet<int>();
        }

        private void Update()
        {
            FindStarByClick();
        }

        private void FindStarByClick()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;

            var mousePosition = Input.mousePosition;
            var ray = castCamera.ScreenPointToRay(mousePosition);

            // ReSharper disable once Unity.PreferNonAllocApi
            var hits = Physics.SphereCastAll(ray, sphereCastRadius);
            if (hits.Length <= 0) return;

            var closestDistance = Mathf.Infinity;
            RaycastHit? closestHit = null;
            foreach (var hit in hits)
            {
                var hitPosition = hit.transform.position;
                var closestPoint = ray.GetPoint(Vector3.Dot(hitPosition - ray.origin, ray.direction));
                var distance = Vector3.Distance(closestPoint, hitPosition);

                if (!(distance < closestDistance)) continue;

                closestDistance = distance;
                closestHit = hit;
            }

            var hitObject = closestHit!.Value.collider.gameObject;
            CreatePanel(hitObject, mousePosition);
        }

        public StarInfo CreatePanel(GameObject hitObject, Vector3 mousePosition)
        {
            var starData = hitObject.GetComponent<StarDataHolder>().Star;
            var hrNumber = starData.DataCompilation.Bsc5Star.HrNumber;
            if(_openedStars.Contains(hrNumber)) return null;
            _openedStars.Add(hrNumber);
            
            Debug.Log($"Opening info for {hitObject}");
            var panel = Instantiate(panelPrefab, hitObject.transform.position, Quaternion.identity, transform);
            var rectTransform = panel.GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(mousePosition.x >= Screen.width / 2d ? 1 : 0,
                mousePosition.y >= Screen.height / 2d ? 1 : 0);

            var starInfo = panel.GetComponent<StarInfo>();
            starInfo.Initialize(starData, () => _openedStars.Remove(hrNumber));
            return starInfo;
        }
    }
}