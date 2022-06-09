using System.Linq;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class CoolnessSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Coolness",
            "Невозмутимость"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap>.\n[Purge:] Gain 1 <ap> for each <bl> on all characters",
            "Получите 2<ap>.\n{Purge:] Получите 1<ap> за каждое <bl> на всех персонажах"
        );
        public override SpellType Type => SpellType.Blood;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, Burst));
        }

        public override void OnPurge()
        {
            base.OnPurge();
            int armor = GameBoard.Player.PereodicDamage.Count + GameBoard.EnemyPool.ActiveEnemies.Sum(enemy => enemy.PereodicDamage.Count);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, armor, Burst), 0);
        }
    }
}