using System;
using System.Collections.Generic;
using Essentials;
using Model.Cards;
using Model.Combat.Effects;
using Model.Combat.Effects.Inner;
using Model.Combat.Shapeshifting;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class Turn : Transformer
    {
        [SerializeField] private GameBoard _gameBoard;

        private ShapeshifterForm _currentForm;
        
        public bool IsPlayerTurn { get; private set; } = true;
        public int CardsPlayedThisTurn { get; private set; }

        public event Action OnPlayerTurnStart;
        public event Action OnPlayerTurnEnd;
        public event Action OnEnemyTurnStart;

        public void AddCardPlayed()
        {
            CardsPlayedThisTurn++;
        }

        private async void EndTurn()
        {
            if (_gameBoard.EffectQueue.EffectInProgress || _gameBoard.TargetChooser.ChooseActive || !IsPlayerTurn)
                return;
            
            OnPlayerTurnEnd?.Invoke();
            await _gameBoard.EffectQueue.WaitForEffects();
            
            IsPlayerTurn = false;
            List<CardOnBoard> cardsToPurge = await _gameBoard.TargetChooser.StartTargetsChoose<CardOnBoard>(_gameBoard.PlayerDiscardPile.transform, 3, true);
            foreach (var card in cardsToPurge)
            {
                _gameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, card));
            }

            await _gameBoard.EffectQueue.WaitForEffects();
            
            DrawCardsUpToMax();

            await _gameBoard.EffectQueue.WaitForEffects();

            CardsPlayedThisTurn = 0;
            OnEnemyTurnStart?.Invoke();

            _gameBoard.EffectQueue.AddEffect(new StartTurnEffect(this, 0.5f));
        }

        private void DrawCardsUpToMax()
        {
            int cardsToDraw = Mathf.Max(5 - _gameBoard.PlayerHand.Size, 0);
            _gameBoard.PlayerDeck.DrawCards(cardsToDraw);
        }

        [DontCallFromSpells]
        public async void StartTurn()
        {
            if (IsPlayerTurn)
                return;

            ShapeshifterForm newForm = await _gameBoard.TargetChooser.StartTargetChoose<ShapeshifterForm>
                (_gameBoard.Player.transform, true);
            
            _currentForm?.OnExit();
            newForm.OnEnter();
            _currentForm = newForm;
            IsPlayerTurn = true;
            OnPlayerTurnStart?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                EndTurn();
            }
        }

        private void Start()
        {
            DrawCardsUpToMax();
        }
    }
}