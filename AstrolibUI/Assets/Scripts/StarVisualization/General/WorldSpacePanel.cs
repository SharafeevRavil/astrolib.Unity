using UnityEngine;

namespace StarVisualization.General
{
    public class WorldSpacePanel : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            //transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
            var cameraRotation = _camera.transform.rotation;
            transform.LookAt(transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);
        }
    }
}