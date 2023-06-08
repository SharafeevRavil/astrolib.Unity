using TMPro;
using UnityEngine;

namespace StarVisualization
{
    public class LineRendererText : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        public void CreateTextsAlongLine(LineRenderer lineRenderer, GameObject textPrefab, int textCount, string text, bool toEnd)
        {
            _lineRenderer = lineRenderer;

            var lineLength = GetLineLength();

            // Создаем текстовые объекты вдоль линии
            for (var i = 0; i < textCount; i++)
            {
                var distanceAlongLine = toEnd
                    ? (i + 1) * (lineLength / textCount)
                    : (i + 1) * (lineLength / (textCount + 1));

                var position = GetPositionOnLine(distanceAlongLine);

                var textObject = Instantiate(textPrefab, position, Quaternion.identity);
                textObject.transform.parent = transform;
                textObject.AddComponent<WorldSpacePanel>();
                
                var tmpText = textObject.GetComponentInChildren<TMP_Text>();
                tmpText.text = text;
            }
        }

        private float GetLineLength()
        {
            var lineLength = 0f;

            // Получаем длину линии путем суммирования расстояний между точками
            for (var i = 0; i < _lineRenderer.positionCount - 1; i++)
                lineLength += Vector3.Distance(_lineRenderer.GetPosition(i), _lineRenderer.GetPosition(i + 1));

            return lineLength;
        }

        private Vector3 GetPositionOnLine(float distance)
        {
            var currentDistance = 0f;

            // Находим позицию на линии, соответствующую заданному расстоянию
            for (var i = 0; i < _lineRenderer.positionCount - 1; i++)
            {
                var startPoint = _lineRenderer.GetPosition(i);
                var endPoint = _lineRenderer.GetPosition(i + 1);
                var segmentDistance = Vector3.Distance(startPoint, endPoint);

                if (currentDistance + segmentDistance >= distance)
                {
                    var t = (distance - currentDistance) / segmentDistance;
                    return Vector3.Lerp(startPoint, endPoint, t);
                }

                currentDistance += segmentDistance;
            }

            // Возвращаем конечную точку, если расстояние больше длины линии
            return _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        }
    }
}