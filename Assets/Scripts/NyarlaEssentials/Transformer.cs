﻿using UnityEngine;

namespace NyarlaEssentials
{
    public class Transformer : MonoBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;
        
        public new Transform transform => _transform ??= gameObject.transform;
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
    }
}