using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class EntaglingRootsSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Entagling roots",
            "Опутывающие корни"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn add Roots Growth to your discard pile",
            "[Пассивно:] В конце вашего хода добавьте Рост Корней в сброс."
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private async void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthRootsSpell)), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}