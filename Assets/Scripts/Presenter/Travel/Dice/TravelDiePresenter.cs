using System.Collections.Generic;
using Essentials;
using Essentials.Pointers;
using Model.Travel.Dice;
using UnityEngine;
using View.Travel.Dice;
using PointerType = Essentials.Pointers.PointerType;

namespace Presenter.Travel.Dice
{
    public class TravelDiePresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private TravelDie _model;
        [SerializeField] private TravelDieView _view;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _rollClips;

        private void Awake()
        {
            for (var i = 0; i < _model.Sides.Length; i++)
            {
                _view.InitSide(i, _model.Sides[i]);
            }
            _model.OnRoll += RollView;
            _pointerTarget.OnDown += StartDrag;
            _pointerTarget.OnDragEnd += DragEnd;
            _pointerTarget.OnEnter += Maximize;
            _pointerTarget.OnExit += Minimize;
        }

        private void RollView(int index, TravelDieSide side)
        {
            _view.RollToSide(index);
            _audioSource.clip = _rollClips.PickRandomElement();
            _audioSource.Play();
        }

        private void StartDrag(PointerType button, Vector3 contact)
        {
            if (button != PointerType.Left)
                return;
            _pointerTarget.OnExit -= Minimize;
            _view.HighlightChosen();
        }
        
        private void DragEnd(PointerType button)
        {
            if (button != PointerType.Left)
                return;
            if (!_pointerTarget.IsMouseOver)
                Minimize();
            _pointerTarget.OnExit += Minimize;
            _view.HighlightUnchosen();
        }
        
        private void Maximize()
        {
            if (_model.Pause.IsPaused)
                return;
            _view.TargetScale = 1.2f;
        }

        private void Minimize()
        {
            if (_model.Pause.IsPaused)
                return;
            _view.TargetScale = 1;
        }
    }
}