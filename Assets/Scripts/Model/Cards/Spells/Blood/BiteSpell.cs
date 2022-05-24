﻿using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells
{
    public class BiteSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bite",
            "Укус"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy and apply 3<bl> to it",
            "Наносит 2<dm> противнику и кладывает на него 3<bl>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            Enemy target = await GetTarget<Enemy>(true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, growth));
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 3, growth));
        }
    }
}