using Model.Cards.Combat;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class ReadyActionEffect : Effect
    {
        private readonly CardOnBoard _cardOnBoard;

        public ReadyActionEffect(float dealyAfter, CardOnBoard cardOnBoard) : base(dealyAfter)
        {
            _cardOnBoard = cardOnBoard;
        }

        public override void Execute()
        {
            _cardOnBoard.Spell.ReadyAction();
        }
    }
}