using UnityEngine;

namespace Updaters
{
    public class MaterialTextureOffsetUpdater : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private const int OffsetX = 0;

        [SerializeField] private float _scrollSpeed = 0.5f;
        [SerializeField] private Renderer _renderer;

        private void Update()
        {
            var offsetY = Time.time * _scrollSpeed;
            var offsetVector = new Vector2(OffsetX, offsetY);
            
            _renderer.material.SetTextureOffset(MainTex, offsetVector);
        }
    }
}