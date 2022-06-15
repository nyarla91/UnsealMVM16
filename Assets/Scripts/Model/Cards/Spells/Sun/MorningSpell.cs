using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class MorningSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Morning",
            "Утро"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Burst:] Draw 4 <cr>.",
            "[Вспышка:] Возьмите 4 <cr>."
        );
        public override SpellType Type => SpellType.Sun;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            if (!burst)
                return;
            for (int i = 0; i < 4; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.05f, GameBoard.PlayerDeck));
            }
        }
    }
}