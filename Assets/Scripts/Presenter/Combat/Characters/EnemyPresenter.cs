using Essentials;
using Essentials.Pointers;
using Model.Combat.Characters.Enemies;
using Model.Localization;
using UnityEngine;
using View.Combat.Characters;

namespace Presenter.Combat.Characters
{
    public class EnemyPresenter : Transformer
    {
        [SerializeField] private Enemy _model;
        [SerializeField] private EnemyView _view;
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _description;

        private void Awake()
        {
            _view.UpdateValues(_model);
            _model.OnDeath += () => _view.Ascend(true, () => Destroy(gameObject));
            _model.OnActivation += _view.Raise;
            _model.OnDeactivation += _view.Drop;
            _pointerTarget.OnEnter += OnEnter;
            _pointerTarget.OnExit += OnExit;
        }

        private void OnExit()
        {
            _model.Tooltip.Hide();
        }

        private void OnEnter()
        {
            _model.Tooltip.Show($"{_name.Localized}\n\n{_description.Localized}");
        }
    }
}