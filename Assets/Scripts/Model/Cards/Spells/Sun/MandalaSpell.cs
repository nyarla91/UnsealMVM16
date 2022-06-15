using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class MandalaSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Mandala",
            "Мандала"
        );
        public override LocalizedString Description => new LocalizedString(
            "Discard your hand.\n[Burst:] Gain 2<br>",
            "Сбросьте руку.\n[Burst:] Получите 2<br>"
        );
        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (CardInHand card in GameBoard.PlayerHand.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.05f, card));
            }
            if (Burst)
                GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.05f, GameBoard.PlayerBoard, 2));
        }
    }
}