using System;
using Essentials.Pointers;
using Model.Global;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Deckbulding
{
    public class ReturnButton : MonoBehaviour
    {
        [SerializeField] protected PointerTarget _pointerTarget;

        [Inject] protected SceneLoader _sceneLoader;
        
        protected virtual void Awake()
        {
            _pointerTarget.OnClick += OnClick;
        }

        protected virtual void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;
            
            _sceneLoader.LoadTravel();
        }
    }
}