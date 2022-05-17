using System;
using System.Collections.Generic;
using Model.Combat.Actions;
using Model.Combat.Cards;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class Turn : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;

        public async void EndTurn()
        {
            List<CardOnBoard> cardsToPurge = await _gameBoard.TargetChooser.StartTargetsChoose<CardOnBoard>(3, true);
            foreach (var card in cardsToPurge)
            {
                _gameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, card), false);
            }
            int cardsToDraw = _gameBoard.PlayerHand.MaxSize - _gameBoard.PlayerHand.Size;
            _gameBoard.PlayerDeck.DrawCards(cardsToDraw);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                EndTurn();
            }
        }
    }
}