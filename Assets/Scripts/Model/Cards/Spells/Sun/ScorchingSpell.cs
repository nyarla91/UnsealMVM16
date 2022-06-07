﻿using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class ScorchingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Scorching",
            "Опаление"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm>. You may discard a <cr> to deal 3<dm> more",
            "Наносит 2<dm>. Вы можете сбросить <cr>, чтобы нанести на 3<dm> больше."
        );
        public override SpellType Type => SpellType.None;
        public override async void OnPlay(bool burst)
        {
            int damage = 2;
            CardInHand cardToDiscard = await GetTarget<CardInHand>(ChooseCardToDiscard, false);
            if (cardToDiscard != null)
            {
                GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.1f, cardToDiscard));
                damage += 3;
            }
            Enemy enemy = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (enemy != null)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, enemy, damage, burst));
            }
            base.OnPlay(burst);
        }
    }
}