using System.Collections.Generic;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class PotionOfMadessSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Potion of madness",
            "Зелье безумия"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nPurge up to 2<cr>",
            "Получите 1<tx.\nОчистите до 2cr>"
        );
        public override SpellType Type => SpellType.Moon;

        private LocalizedString PurgeTwocardsMessage => new LocalizedString(
            "Choose up to 2 cards to purge",
            "Выберите до 2 карт, которые хотите очистить"
        );
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            List<CardOnBoard> cardToPurge = await 
                GetTargets<CardOnBoard>(PurgeTwocardsMessage, 2, false);
            if (cardToPurge.Count == 0)
                return;
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            foreach (CardOnBoard card in cardToPurge)
            {
                GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.05f, card));
            }
        }
    }
}