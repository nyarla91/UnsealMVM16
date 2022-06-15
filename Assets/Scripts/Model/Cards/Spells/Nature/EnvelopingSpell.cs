using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class EnvelopingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Enveloping",
            "Окутывание"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1 Roots Growth to your discard pile. If your have at least 5 <cr> in your hand add 3 more",
            "Добавьте 1 Рост Корней в ваш сброс. Если у вас есть хотя бы 5 <cr> в руке, добавьте ещё 3."
        );
        
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int growthsToAdd = GameBoard.PlayerHand.Size >= 5 ? 4 : 1;
            for (int i = 0; i < growthsToAdd; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthRootsSpell)));
            }
        }
    }
}