using UnityEngine;

namespace Updaters
{
    public class LookUpdater : MonoBehaviour
    {
        private const string AxisX = "Mouse X";
        private const string AxisY = "Mouse Y";
        private const float MinAngleX = -89f;
        private const float MaxAngleX = 89f;
        private const float DefaultRotation = 0f;
        private const float MinAxisValue = -1f;
        private const float MaxAxisValue = 1f;

        [SerializeField, Header("Refs")] private Transform _target;
        [SerializeField, Header("Settings")] private float _mouseSensitivity = 20f;

        private float _xRotation;
        private float _yRotation;
    
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            var rotation = _target.rotation.eulerAngles;
            _xRotation = rotation.x;
            _yRotation = rotation.y;
            _target.rotation = Quaternion.Euler(_xRotation, _yRotation, DefaultRotation);
        }

        private void Update()
        {
            var mouseX = Mathf.Clamp(Input.GetAxis(AxisX), MinAxisValue, MaxAxisValue) * _mouseSensitivity * Time.deltaTime;
            var mouseY = Mathf.Clamp(Input.GetAxis(AxisY), MinAxisValue, MaxAxisValue) * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY * _mouseSensitivity;
            _xRotation = Mathf.Clamp(_xRotation, MinAngleX, MaxAngleX);

            _yRotation += mouseX * _mouseSensitivity;

            _target.rotation = Quaternion.Euler(_xRotation, _yRotation, DefaultRotation);
        }
    }
}