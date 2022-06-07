﻿using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class SpikedVineSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Spiked vine",
            "Колючая лоза"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 1<dm> three times",
            "Трижды наносит 1<dm>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            for (int i = 0; i < 3; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, target, 1, burst));
            }
        }
    }
}