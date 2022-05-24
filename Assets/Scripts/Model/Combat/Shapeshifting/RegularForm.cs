using Model.Cards.Spells;
using UnityEngine;

namespace Model.Combat.Shapeshifting
{
    public class RegularForm : ShapeshifterForm
    {
        public override void OnEnter()
        {
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }

        public override void OnExit() { }
    }
}