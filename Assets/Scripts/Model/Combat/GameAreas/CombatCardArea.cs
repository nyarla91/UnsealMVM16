using Model.Cards;
using Model.Cards.Combat;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public abstract class CombatCardArea<TCard>: CardArea<TCard> where TCard : CardInCombat
    {
        [SerializeField] private GameBoard _gameBoard;

        protected GameBoard GameBoard => _gameBoard;
        
        protected override void PassBoard(TCard cardInPlace)
        {
            cardInPlace.GameBoard = _gameBoard;
            cardInPlace.Spell.GameBoard = _gameBoard;
        }
    }
}