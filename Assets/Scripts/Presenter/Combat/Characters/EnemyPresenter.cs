using System;
using Essentials;
using Model.Combat.Characters;
using UnityEngine;
using View.Combat.Characters;

namespace Presenter.Combat.Characters
{
    public class EnemyPresenter : Transformer
    {
        [SerializeField] private Enemy _model;
        [SerializeField] private EnemyKind _kind;
        [SerializeField] private EnemyView _view;

        private void Awake()
        {
            _view.UpdateValues(_kind);
            _model.OnDeath += () => _view.Ascend(true, () => Destroy(gameObject));
        }
    }
}