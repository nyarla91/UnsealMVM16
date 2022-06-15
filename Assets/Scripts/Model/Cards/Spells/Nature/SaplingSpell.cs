using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class SaplingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sapling",
            "Саженец"
        );
        public override LocalizedString Description => new LocalizedString(
            "Restore 2<hp>.\nAdd Blossom Growth to your discard pile.",
            "Восстанавливает вам 2<hp>\nДобавьте Рост Цветков в ваш сброс"
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 2, burst));
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
        }
    }
}