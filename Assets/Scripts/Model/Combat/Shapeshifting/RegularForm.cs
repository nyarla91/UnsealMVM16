using Model.Cards.Spells;
using UnityEngine;

namespace Model.Combat.Shapeshifting
{
    public class RegularForm : Form
    {
        public override void Enter()
        {
            base.Enter();
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}