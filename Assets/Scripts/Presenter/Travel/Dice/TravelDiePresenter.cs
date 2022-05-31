using Essentials.Pointers;
using Model.Travel.Dice;
using UnityEngine;
using View.Travel.Dice;

namespace Presenter.Travel.Dice
{
    public class TravelDiePresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private TravelDie _model;
        [SerializeField] private TravelDieView _view;

        private void Awake()
        {
            for (var i = 0; i < _model.Sides.Length; i++)
            {
                _view.InitSide(i, _model.Sides[i]);
            }
            _model.OnRoll += RollView;
            _pointerTarget.OnEnter += Maximize;
            _pointerTarget.OnExit += Minimize;
        }

        private void RollView(int index, TravelDieSide side)
        {
            _view.RollToSide(index);
        }

        private void Maximize()
        {
            _view.TargetScale = 1.2f;
        }

        private void Minimize() => _view.TargetScale = 1;
    }
}