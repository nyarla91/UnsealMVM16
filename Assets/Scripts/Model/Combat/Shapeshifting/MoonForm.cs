using Model.Cards.Spells;
using Model.Combat.Effects;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Model.Combat.Shapeshifting
{
    public class MoonForm : Form
    {
        public override void Enter()
        {
            base.Enter();
            GameBoard.PlayerBoard.OnCardPurged += OnCardPurged;
            GameBoard.PlayerHand.ForbiddenType = SpellType.Sun;
        }

        private void OnCardPurged(Spell card)
        {
            if (GameBoard.Turn.IsPlayerTurn)
            {
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.05f, GameBoard.Player, 3, false), 0);
            }
        }

        public override void Exit()
        {
            base.Exit();
            GameBoard.PlayerBoard.OnCardPurged -= OnCardPurged;
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}