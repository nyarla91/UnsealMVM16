using UnityEngine;

namespace Essentials
{
    public abstract class Transformer : MonoBehaviour
    {
        private Transform _transform;
        public new Transform transform => _transform ??= gameObject.transform;

        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
    }
}