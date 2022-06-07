using System;
using Essentials.Pointers;
using Model.Deckbulding;
using UnityEngine;

namespace Presenter.Deckbuilding
{
    public class SaveAndReturnButtonPresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private SaveAndReturnButton _model;

        private void Awake()
        {
            _pointerTarget.OnEnter += OnEnter;
            _pointerTarget.OnExit += OnExit;
        }

        private void OnExit()
        {
            
        }

        private void OnEnter()
        {
            
        }
    }
}