using Model.Cards.Spells;
using UnityEngine;

namespace Model.Combat.Shapeshifting
{
    public class SunForm : ShapeshifterForm
    {
        public override void OnEnter()
        {
            GameBoard.PlayerBoard.PassiveModifier = 2;
            GameBoard.PlayerHand.ForbiddenType = SpellType.Moon;
        }

        public override void OnExit()
        {
            GameBoard.PlayerBoard.PassiveModifier = 1;
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}