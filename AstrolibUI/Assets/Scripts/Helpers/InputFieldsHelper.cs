using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Helpers
{
    public class InputFieldsHelper : MonoBehaviour
    {
        private TMP_InputField[] _inputFields;
        
        private void Start()
        {
            _inputFields = FindObjectsOfType<TMP_InputField>();
        }

        public bool IsAnyFocused() => _inputFields.Any(inputField => inputField.isFocused);
    }
}