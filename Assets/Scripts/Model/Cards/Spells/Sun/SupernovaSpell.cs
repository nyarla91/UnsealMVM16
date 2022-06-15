using System.Collections.Generic;
using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class SupernovaSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Supernova",
            "Сверхновая"
        );
        public override LocalizedString Description => new LocalizedString(
            "Discard any number of cards. Deal 1 <dm> to all enemies for each one.",
            "Сбросьте любое количество карт. Наносит 1<dm> всем противникам за каждую."
        );
        public override SpellType Type => SpellType.Sun;
        
        private LocalizedString DiscardAnyCardsMessage => new LocalizedString(
            "Discard any cards",
            "Сбросьте любые карты"
        );
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            List<CardInHand> cardsToDiscard = await GetTargets<CardInHand>(DiscardAnyCardsMessage, 15, false);
            foreach (var card in cardsToDiscard)
            {
                GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.05f, card));
                foreach (Enemy enemy in GameBoard.EnemyPool.ActiveEnemies)
                {
                    GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, enemy, 1, Burst));
                }
            }
        }
    }
}