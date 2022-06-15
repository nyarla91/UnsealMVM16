using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class MedicinalHerbsSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "medicinal herbs",
            "Целебные травы"
        );

        public override LocalizedString Description => new LocalizedString(
            "Cure 1<tx>.\nAdd 1 Blossom Growth to your discard pile.",
            "Вылечите 1<tx>\n.Добавьте 1 Рост Цветков в сброс."
        );

        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
            GameBoard.EffectQueue.AddEffect(new CureIntoxicationEffect(0.1f, GameBoard.Player, 1));
        }
    }
}