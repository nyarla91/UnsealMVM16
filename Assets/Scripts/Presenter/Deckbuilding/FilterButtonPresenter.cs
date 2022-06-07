using System;
using Model.Deckbulding.Filters;
using UnityEngine;
using View.Deckbuilding;

namespace Presenter.Deckbuilding
{
    public class FilterButtonPresenter : MonoBehaviour
    {
        [SerializeField] private FilterButton _model;
        [SerializeField] private FilterButtonView _view;

        private void Awake()
        {
            _model.OnSwitch += OnSwitch;
        }

        private void OnSwitch(bool active)
        {
            if (active)
                _view.Press();
            else
                _view.Release();
        }
    }
}