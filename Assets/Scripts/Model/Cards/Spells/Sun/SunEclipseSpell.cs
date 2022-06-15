using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Sun
{
    public class SunEclipseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sun eclipse",
            "Солнечное затмение"
        );

        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy. You may discard a <cr> deal 5<dm> instead",
            "Наносит 2<dm> противнику. Вы можете сбросить <cr>, чтобы вместо этого нанести 5<dm>"
        );

        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy enemy = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            CardInHand cardToDiscard = await GetTarget<CardInHand>(ChooseCardToDiscardMessage, false);

            int damage = cardToDiscard == null ? 5 : 2;
            GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.1f, cardToDiscard), 0);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, enemy, damage, Burst), 1);
        }
    }
}