using UnityEngine;

namespace Essentials
{
    public abstract class RectTransformer : MonoBehaviour
    {
        private RectTransform _rectTransform;
        
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
    }
}