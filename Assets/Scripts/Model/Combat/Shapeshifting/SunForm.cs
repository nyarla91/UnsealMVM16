using Model.Cards.Spells;
using UnityEngine;

namespace Model.Combat.Shapeshifting
{
    public class SunForm : Form
    {
        public override void Enter()
        {
            base.Enter();
            GameBoard.PlayerBoard.PassiveModifier = 2;
            GameBoard.PlayerHand.ForbiddenType = SpellType.Moon;
        }

        public override void Exit()
        {
            base.Exit();
            GameBoard.PlayerBoard.PassiveModifier = 1;
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}