using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class LakePortalSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Lake portal",
            "Портал а озере"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1<ch> to a card on the board\n[Draw, Discard, Purge:] Do the same",
            "Добавьте 1<ch> на карту на поле.\n[Взятие, Сброс, Очищение:] Повторите эффект"
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            AddChargeOnTheCard();
        }

        public override void OnDiscard()
        {
            base.OnDiscard();
            AddChargeOnTheCard();
        }

        public override void OnDraw()
        {
            base.OnDraw();
            AddChargeOnTheCard();
        }

        public override void OnPurge()
        {
            base.OnPurge();
            AddChargeOnTheCard();
        }

        private async void AddChargeOnTheCard()
        {
            CardOnBoard cardOnBoard = await GetTarget<CardOnBoard>(ChooseCardMessage, true);
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, cardOnBoard.Spell, 1), 0);
        }
    }
}