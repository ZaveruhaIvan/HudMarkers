using System;
using Models;
using UnityEngine;
using Updaters;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        private const int CurrentSwapPont = 0;
        
        [field: SerializeField] public MarkerViewsContainer MarkerViewsContainer { get; private set; }
        [SerializeField] private CameraTransformUpdater _cameraTransformUpdater;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                if (_cameraTransformUpdater.SwapPointsAmount != null)
                {
                    SwapPointsModel = new SwapPointsModel(_cameraTransformUpdater.SwapPointsAmount.Value, CurrentSwapPont);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public static GameView Instance { get; private set; }
        public SwapPointsModel SwapPointsModel { get; private set; }
    }
}