using Essentials;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace View.Travel
{
    public class MapBackground : Transformer
    {
        [SerializeField] private Transform _cameraOrigin;

        private void Update()
        {
            transform.position = _cameraOrigin.position.SnapToGrid(Vector3.one * 7.5f).WithY(-0.05f);
        }
    }
}