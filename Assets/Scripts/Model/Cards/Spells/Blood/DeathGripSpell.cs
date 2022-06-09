using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class DeathGripSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Death grip",
            "Мёртвая хватка"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\nDeal 1 additional <dm> for each its <bl>",
            "Наносит 2<dm> противнику.\nНаносит на 1<dm> больше за каждое его <bl>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;

            int damage = 2 + target.PereodicDamage.Count;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage, burst));
        }
    }
}