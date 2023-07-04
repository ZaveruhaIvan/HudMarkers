using UnityEngine;
using Random = UnityEngine.Random;

namespace Views
{
    public class RandomObjectSpawnerView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _grassPrefabs;

        private void Awake()
        {
            var randomIndex = Random.Range(0, _grassPrefabs.Length);
            Instantiate(_grassPrefabs[randomIndex], gameObject.transform);
        }
    }
}