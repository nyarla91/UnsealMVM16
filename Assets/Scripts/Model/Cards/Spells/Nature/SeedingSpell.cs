using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class SeedingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seeding",
            "Засев"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you use action add 1 Blossom Growth to your discard pile",
            "[Пассивно:] Когда вы используете действие добавляйте 1 Рост Цветков в сброс."
        );
        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerBoard.OnActionUsed += OnActionUsed;
        }

        private void OnActionUsed(Spell spell)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
            }
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.PlayerBoard.OnActionUsed -= OnActionUsed;
        }
    }
}