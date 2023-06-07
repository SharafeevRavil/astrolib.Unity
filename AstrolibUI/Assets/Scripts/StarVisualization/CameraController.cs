using UnityEngine;

namespace StarVisualization
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 3f; // Скорость вращения камеры
        [SerializeField] private float verticalLimit = 80f; // Ограничение на вертикальный угол обзора
        [SerializeField] private float mouseRotationSpeed = 50f; // Скорость вращения камеры мышью

        private float _rotationX;

        private void Update()
        {
            RotateByKeyboard();
            RotateByMouse();
        }

        private void RotateByMouse()
        {
            if (!Input.GetKey(KeyCode.Mouse1)) return;
            
            var mouseX = Input.GetAxis("Mouse X") * mouseRotationSpeed;
            var mouseY = Input.GetAxis("Mouse Y") * mouseRotationSpeed;

            mouseX *= -1;
            mouseY *= -1;
            
            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
            _rotationX -= mouseY * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -verticalLimit, verticalLimit);
            transform.localRotation = Quaternion.Euler(_rotationX, transform.localEulerAngles.y, 0f);
        }

        private void RotateByKeyboard()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
            
            _rotationX -= verticalInput * rotationSpeed * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -verticalLimit, verticalLimit);
            transform.localRotation = Quaternion.Euler(_rotationX, transform.localEulerAngles.y, 0f);
        }
        /*private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                var cameraTransform = viewCamera.transform;
                var cameraPosition = cameraTransform.position;
                cameraTransform.RotateAround(cameraPosition, cameraTransform.right, Input.GetAxis("Mouse Y"));
                cameraTransform.RotateAround(cameraPosition, Vector3.up, -Input.GetAxis("Mouse X"));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                constellationVisualizer.Visualize(_stars, starFieldScale);
            }
        }*/
    }
}