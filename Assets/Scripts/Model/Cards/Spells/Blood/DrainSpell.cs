using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class DrainSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Drain",
            "Высасывание"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\nRestore 2<hp>",
            "Наносит 2<dm> противнику.\nВосстаносите 2<hp>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;

            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, burst));
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 2, burst));
        }
    }
}