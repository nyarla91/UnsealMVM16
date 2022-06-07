using System;
using Essentials;
using Model.Combat.GameAreas;
using Model.Combat.Targeting;
using Model.Localization;
using UnityEngine;
using Zenject;

namespace Model.Combat.Shapeshifting
{
    public abstract class Form : Transformer
    {
        [SerializeField] private TargetToChoose _targetToChoose;
        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _description;
        private GameBoard _gameBoard;

        [Inject]
        public GameBoard GameBoard
        {
            get => _gameBoard;
            set
            {
                _targetToChoose.GameBoard = value;
                _gameBoard = value;
            }
        }

        public LocalizedString Name => _name;
        public LocalizedString Description => _description;

        public event Action OnEnter;
        public event Action OnExit;

        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }

        public virtual void Exit()
        {
            OnExit?.Invoke();
        }
    }
}