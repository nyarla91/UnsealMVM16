using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class BloomingRoseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blooming rose",
            "Цветущая роза"
        );

        public override LocalizedString Description => new LocalizedString(
            "Add 1 Blossom Growth to your discard pile.\n[Passive:] At the end of your turn draw 2<cr>",
            "Добавьте 1 Рост Цветков в сброс.\n[Пассивно:] В конце хода возьмите 2<cr>"
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthBlossomSpell)));
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private async void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier * 2; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}