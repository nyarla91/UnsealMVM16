using System.Linq;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class ThunderboltSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Thundebolt",
            "Раскат грома"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Discard:] if you cannot play this card due to form deal 4<dm> to all enemies",
            "[Сброс:] Если вы не можете сыграть эту карту из-за облика наносит 4<dm> всем противникам."
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnDiscard()
        {
            base.OnDiscard();
            if (GameBoard.PlayerHand.ForbiddenType != Type)
                return;
            foreach (Enemy target in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 4, false), 0);
            }
        }
    }
}