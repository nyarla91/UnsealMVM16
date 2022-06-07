using Essentials.Pointers;
using Model.Travel.Dice;
using UnityEngine;
using View;
using PointerType = Essentials.Pointers.PointerType;

namespace Presenter.Travel
{
    public class TurnButtonPresenter : MonoBehaviour
    {
        [SerializeField] private TravelDicePool _dicePool;
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private ButtonView _view;

        private void Awake()
        {
            _pointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;
            
            _view.Press();
            _dicePool.NextTurn();
        }
    }
}