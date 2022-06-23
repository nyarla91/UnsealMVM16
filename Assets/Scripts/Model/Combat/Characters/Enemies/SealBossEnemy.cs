using Model.Cards.Combat;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class SealBossEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
            GameBoard.Turn.OnPlayerTurnStart += OnPlayerTurnStart;
        }

        private void OnPlayerTurnStart()
        {
            foreach (CardInHand card in GameBoard.PlayerHand.Cards)
            {
                if (card.Spell.Type == GameBoard.PlayerHand.ForbiddenType)
                    GameBoard.EffectQueue.AddEffect(new LoseArmorEffect(0.1f, this, 5));
            }
        }
    }
}