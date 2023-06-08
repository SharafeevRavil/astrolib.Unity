using System;
using System.Collections.Generic;
using System.Linq;
using StarVisualization.Controllers;
using StarVisualization.Stars;
using StarVisualization.UiFeatures.StarInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace StarVisualization.UiFeatures
{
    public class SearchStars : MonoBehaviour
    {
        [SerializeField] private StarSky starSky;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private StarInfoOpen starInfoOpen;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text resultsText;

        private string _lastSearch;
        private List<GameObject> _foundStars;
        private int _currentIndex;
        private StarInfo.StarInfo _lastOpened;

        private void Start()
        {
            _lastSearch = null;
            button.onClick.AddListener(Search);

            resultsText.gameObject.SetActive(false);
            inputField.onValueChanged.AddListener(_ => resultsText.gameObject.SetActive(false));
        }


        private static bool StarMatches(Star star, string searchStr) =>
            star.FullName?.IndexOf(searchStr, StringComparison.OrdinalIgnoreCase) > -1;

        public void Search()
        {
            var curSearch = inputField.text;
            if (_lastSearch != curSearch)
            {
                var foundStarsWithIndex = starSky.Stars
                    .Select((x, i) => (Star: x, Index: i))
                    .Where(x => StarMatches(x.Star, curSearch));
                _foundStars = foundStarsWithIndex
                    .Select(x => starSky.StarObjects[x.Index])
                    .ToList();
                _lastSearch = curSearch;
                _currentIndex = 0;
            }
            else
            {
                if (_foundStars.Count == 0) return;
                _currentIndex++;
                _currentIndex %= _foundStars.Count;
            }

            resultsText.text =
                $"Showing {(_foundStars.Count == 0 ? 0 : _currentIndex + 1)} of {_foundStars.Count} stars";
            resultsText.gameObject.SetActive(true);
            if (_foundStars.Count == 0) return;
            //Look at star
            cameraController.LookAtPoint(_foundStars[_currentIndex].transform.position);
            //Open panel
            if (_foundStars.Count != 1 && _lastOpened != null) _lastOpened.Close();
            var newInfo = starInfoOpen.CreatePanel(_foundStars[_currentIndex].gameObject,
                new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0));
            if (newInfo != null)
                _lastOpened = newInfo;
        }
    }
}