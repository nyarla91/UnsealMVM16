using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class WorldTreeSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "World tree",
            "Мировое древо"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1 Blossom Growth, 1 Roots Growth and 1 Thorns Growth to your discard pile",
            "Добавьте 1 Рост Цветков, 1 Рост Корни и 1 Рост Шипов в ваш сброс."
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthRootsSpell)));
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthThornsSpell)));
        }
    }
}