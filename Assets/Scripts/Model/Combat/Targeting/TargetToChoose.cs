using System;
using Essentials.Pointers;
using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Combat.Targeting
{
    public class TargetToChoose : MonoBehaviour
    {
        private PointerTarget _pointerTarget;

        private bool _chooseAvailable;
        
        [Inject] private GameBoard _gameBoard;

        public GameBoard GameBoard
        {
            get => _gameBoard;
            set
            {
                if (_gameBoard != null)
                    return;
                
                _gameBoard = value;
                _gameBoard.TargetChooser.AddTarget(this);
            }
        }

        public event Action OnStartChoosing;
        public event Action OnChooseSwitch;
        public event Action OnEndChoosing;

        public void StartChoose()
        {
            _chooseAvailable = true;
            OnStartChoosing?.Invoke();
        }

        public void EndChoose()
        {
            _chooseAvailable = false;
            OnEndChoosing?.Invoke();
        }

        private void Awake()
        {
            _pointerTarget = GetComponent<PointerTarget>();
            _pointerTarget.OnClick += OnClick;
            if (_gameBoard != null)
            {
                _gameBoard.TargetChooser.AddTarget(this);
            }
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left || !_chooseAvailable)
                return;

            if (GameBoard.TargetChooser.TargetChosen(this))
            {
                GameBoard.TargetChooser.RemoveChosenTarget(this);
                OnChooseSwitch?.Invoke();
            }
            else if (GameBoard.TargetChooser.TryAddChosenTarget(this))
            {
                OnChooseSwitch?.Invoke();
            }
        }

        private void OnDestroy()
        {
            GameBoard?.TargetChooser.RemoveTarget(this);
        }
    }
}