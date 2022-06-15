using System.Collections.Generic;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class RemoveTheMaskSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Remove the mask",
            "Снять маску"
        );
        public override LocalizedString Description => new LocalizedString(
            "Purge any number of <cr>. Spend 2<ap> for each one",
            "Очистите любое количество <cr>. Потратьте 2<ap> за каждую."
        );
        public override SpellType Type => SpellType.Moon;
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int maxCardPossible = GameBoard.Player.Armor / 2;
            List<CardOnBoard> cardToPurge = await 
                GetTargets<CardOnBoard>(ChooseCardsMessage(maxCardPossible), maxCardPossible, false);
            if (cardToPurge.Count == 0)
                return;
            GameBoard.EffectQueue.AddEffect(new LoseArmorEffect(0.1f, GameBoard.Player, cardToPurge.Count * 2));
            foreach (CardOnBoard card in cardToPurge)
            {
                GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.05f, card));
            }
        }

        private LocalizedString ChooseCardsMessage(int cards) =>
            new LocalizedString(
                $"Choose up to {cards} card to purge",
                $"Выберите до {cards} карт, которые хотите очистить"
            );
    }
}