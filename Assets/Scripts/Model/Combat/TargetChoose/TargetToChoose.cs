using System;
using Model.Combat.GameAreas;
using NyarlaEssentials.Pointers;
using UnityEngine;
using Zenject;
using PointerType = NyarlaEssentials.Pointers.PointerType;

namespace Model.Combat.TargetChoose
{
    public class TargetToChoose : MonoBehaviour
    {
        [Inject] private GameBoard _board;
        private PointerTarget _pointerTarget;

        private bool _chooseAvailable;

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
        }

        private void Start()
        {
            _board.TargetChooser.AddTarget(this);
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left || !_chooseAvailable)
                return;

            if (_board.TargetChooser.TargetChosen(this))
            {
                _board.TargetChooser.RemoveChosenTarget(this);
                OnChooseSwitch?.Invoke();
            }
            else if (_board.TargetChooser.TryAddChosenTarget(this))
            {
                OnChooseSwitch?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _board.TargetChooser.RemoveTarget(this);
        }
    }
}