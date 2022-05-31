using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Essentials.Pointers
{
    public class PointerTarget : MonoBehaviour
    {
        
        [Tooltip("Period of invoking OnDrag event\n0 or less - every frame")]
        [SerializeField] private float _dragPeriod;
        
        public delegate void MousePointActionHandler(PointerType button, Vector3 contactPoint);
        public MousePointActionHandler OnDown, OnUp, OnClick, OnDoubleClick;
        
        public delegate void MouseActionHandler(PointerType button);
        public MouseActionHandler OnDrag, OnDragEnd;
        
        public Action OnEnter, OnExit;
        
        private Dictionary<PointerType, Coroutine> _dragCoroutines = new Dictionary<PointerType, Coroutine>();

        private void Awake()
        {
            PointerCaster.Instance.AddTarget(this);
            OnDown += (button, contact) => _dragCoroutines.Add(button, StartCoroutine(Drag(button)));
        }

        private IEnumerator Drag(PointerType button)
        {
            while (true)
            {
                OnDrag?.Invoke(button);
                if (_dragPeriod > 0)
                    yield return new WaitForSeconds(_dragPeriod);
                else
                    yield return null;
            }
        }

        public void TryEndDrag(PointerType button)
        {
            if (_dragCoroutines.ContainsKey(button) && _dragCoroutines[button] != null)
            {
                StopCoroutine(_dragCoroutines[button]);
                _dragCoroutines.Remove(button);
                OnDragEnd?.Invoke(button);
            }
        }

        private void OnDestroy()
        {
            PointerCaster.Instance?.RemoveTarget(this);
        }
    }
}