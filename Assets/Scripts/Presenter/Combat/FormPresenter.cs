using System;
using Essentials.Pointers;
using Model.Combat.Shapeshifting;
using UnityEngine;
using View.Cards;
using View.Combat;
using Zenject;

namespace Presenter.Combat
{
    public class FormPresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private Form _model;
        [SerializeField] private FormView _view;

        [field: Inject] public AbilitiyTooltip Tooltip { private get; set; }
        
        private void Awake()
        {
            _model.OnEnter += _view.Flip;
            _model.OnExit += _view.Flip;
            _pointerTarget.OnEnter += ShowTooltip;
            _pointerTarget.OnExit += HideTooltip;
        }

        private void ShowTooltip()
        {
            Tooltip.Show($"{_model.Name}\n \n{_model.Description}\n \n \n \n{Tooltip.DescriptionToExlanation(_model.Description)}");
        }

        private void HideTooltip()
        {
            Tooltip.Hide();
        }
    }
}