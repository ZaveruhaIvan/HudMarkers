using TMPro;
using UnityEngine;

namespace Updaters
{
    public class ModeUpdater : MonoBehaviour
    {
        [SerializeField, Header("Refs")] private BorderMarkersUpdater _borderMarkersUpdater;
        [SerializeField] private EllipseMarkersUpdater _ellipseMarkersUpdater;
        [SerializeField] private TextMeshProUGUI _modeText;
        [Space]
        [SerializeField, Header("Settings")] private string _ellipseModeText = "E-Mode";
        [SerializeField] private Color _ellipseModeTextColor;
        [SerializeField] private string _borderModeText = "S-Mode";
        [SerializeField] private Color _borderModeTextColor;
        
        private void Awake()
        {
            _borderMarkersUpdater.enabled = true;
            _ellipseMarkersUpdater.enabled = false;

            SetModeText();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _borderMarkersUpdater.enabled = !_borderMarkersUpdater.enabled;
                _ellipseMarkersUpdater.enabled = !_ellipseMarkersUpdater.enabled;
                
                SetModeText();
            }
        }

        private void SetModeText()
        {
            if (_borderMarkersUpdater.enabled)
            {
                _modeText.text = _borderModeText;
                _modeText.color = _borderModeTextColor;
            }
            else
            {
                _modeText.text = _ellipseModeText;
                _modeText.color = _ellipseModeTextColor;
            }
        }
    }
}