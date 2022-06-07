﻿using System;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthBlossomSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blossom growth",
            "Рост цветка"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Restore 2<hp>.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Восстановите 2<hp>.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );
        public override SpellType Type => SpellType.Nature;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDraw()
        {
            base.OnDraw();
            GameBoard.EffectQueue.AddEffect(new WaitEffect(0.5f));
            Player target = GameBoard.Player;
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, target, 2, false), 1);
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace), 2);
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 3);
        }
    }
}