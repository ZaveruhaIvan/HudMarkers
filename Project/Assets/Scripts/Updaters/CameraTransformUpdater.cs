using Models;
using TMPro;
using UnityEngine;
using Views;

namespace Updaters
{
    public class CameraTransformUpdater : MonoBehaviour
    {
        private const int IndexOffset = 1;
    
        [SerializeField] private Transform[] _swapPoints;
        [SerializeField] private Transform _camera;
        [SerializeField] private TextMeshProUGUI _currentSwapPointText;

        private SwapPointsModel _spawnPointsModel;
        private int? _swapPointsAmount;
        private int _currentPoint;

        private void Awake()
        {
            _spawnPointsModel = GameView.Instance.SwapPointsModel;
        
            _currentPoint = GetCurrentPoint();
            SetCameraTransform(_currentPoint);
        }

        private void Update()
        {
            UpdateInput();

            var currentPoint = GetCurrentPoint();

            if (currentPoint != _currentPoint)
            {
                SetCameraTransform(currentPoint);
                _currentPoint = currentPoint;
            }
        }

        private void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _spawnPointsModel.SetPoint(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _spawnPointsModel.SetPoint(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _spawnPointsModel.SetPoint(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _spawnPointsModel.SetPoint(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _spawnPointsModel.SetPoint(4);
            }
        }

        private int GetCurrentPoint() => 
            _spawnPointsModel.Current;

        private void SetCameraTransform(int currentPoint)
        {
            var swapPoint = _swapPoints[currentPoint];
            _camera.transform.position = swapPoint.position;
            _currentSwapPointText.text = (currentPoint + IndexOffset).ToString();
        }

        public int? SwapPointsAmount
        {
            get
            {
                if (_swapPointsAmount != null)
                {
                    return _swapPointsAmount;
                }
            
                _swapPointsAmount = _swapPoints.Length;
                return _swapPointsAmount;

            }
        }
    }
}