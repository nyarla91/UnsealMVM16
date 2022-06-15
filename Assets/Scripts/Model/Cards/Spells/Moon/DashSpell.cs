using System.Collections.Generic;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class DashSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Dash",
            "Порыв"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Purge a <cr> for each <ch>. Purge this <cr>.",
            "[Действие:] Очистите <cr> а каждый <ch>. Очистите эту <cr>."
        );
        public override SpellType Type => SpellType.Action;

        public override bool HasAction => true;

        public override async void OnUseAction()
        {
            base.OnUseAction();
            List<CardOnBoard> cardsToPurge = await GetTargets<CardOnBoard>(ChooseCardsToPurgeMessage(Charges), Charges, false);
            cardsToPurge.Add((CardOnBoard) CardPlace);
            foreach (CardOnBoard cardOnBoard in cardsToPurge)
            {
                GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, cardOnBoard));
            }
        }

        private LocalizedString ChooseCardsToPurgeMessage(int cards) => new LocalizedString(
            $"Choose {cards} cards to purge.",
            $"Выбрите {cards} карты, которые хотите очистить"
        );
    }
}