using System;
using Essentials.Pointers;
using Model.Combat.GameAreas;
using Model.Combat.Targeting;
using Model.Localization;
using UnityEngine;
using View.Combat;
using PointerType = Essentials.Pointers.PointerType;

namespace Presenter.Combat
{
    public class CombatButtonPresenter : MonoBehaviour
    {
        [SerializeField] private Turn _turn;
        [SerializeField] private TargetChooser _targetChooser;
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private CombatButtonView _view;
        [SerializeField] private CombatButtonState _inactiveState;
        [SerializeField] private CombatButtonState _endTurnState;
        [SerializeField] private CombatButtonState _skipState;
        
        private bool Pressable { get; set; }
        
        private void Awake()
        {
            _turn.OnPlayerTurnEnd += () => UpdateView(false, _targetChooser.ChooseActive);
            _turn.OnPlayerTurnStart += () => UpdateView(true, _targetChooser.ChooseActive);
            _targetChooser.OnChooseStart += (t, s) => UpdateView(_turn.IsPlayerTurn, true);
            _targetChooser.OnChooseEnd += () => UpdateView(_turn.IsPlayerTurn, false);
            UpdateView(true, false);
            _pointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left || !Pressable)
                return;
            
            _view.Press();
            if (_targetChooser.ChooseActive)
                _targetChooser.SkipButtonPressed = true;
            else if (_turn.IsPlayerTurn)
                _turn.EndTurnButtonPressed = true;
        }

        private void UpdateView(bool playerTurn, bool targetChoose)
        {
            if (targetChoose && !_targetChooser.ExactNumber)
            {
                _view.UpdateState(_skipState);
                Pressable = true;
            }
            else if (playerTurn)
            {
                _view.UpdateState(_endTurnState);
                Pressable = true;
            }
            else
            {
                _view.UpdateState(_inactiveState);
                Pressable = false;
            }
        }
    }
        
    [Serializable]
    public class CombatButtonState
    {
        [SerializeField] private Material material;
        [SerializeField] private LocalizedString _text;

        public Material Material => material;
        public LocalizedString Text => _text;
    } 
}