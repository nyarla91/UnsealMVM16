using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class FallingLeavesSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Falling leaves",
            "Поадющие листья"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1 Blossom Growth to your discard pile. [Draw, Purge, Discard:] Do the same",
            "Добавьте 1 Рост Цветков в сброс. [Взятие, Очищение, Сброс;] Повторите эффект"
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            AddBlossomGrowth();
        }

        public override void OnDiscard()
        {
            base.OnDiscard();
            AddBlossomGrowth();
        }

        public override void OnDraw()
        {
            base.OnDraw();
            AddBlossomGrowth();
        }

        public override void OnPurge()
        {
            base.OnPurge();
            AddBlossomGrowth();
        }

        private void AddBlossomGrowth()
        {
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
        }
    }
}