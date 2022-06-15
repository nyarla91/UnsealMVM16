using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class SunriseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sunrise",
            "Восход"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<br> for each your passive card on the board",
            "Полусите 1<br> за каждую вашу пассивную карту на поле."
        );
        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (CardOnBoard cardOnBoard in GameBoard.PlayerBoard.Cards)
            {
                if (cardOnBoard.Spell.HasPassive)
                    GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.05f, GameBoard.PlayerBoard, 1));
            }
        }
    }
}