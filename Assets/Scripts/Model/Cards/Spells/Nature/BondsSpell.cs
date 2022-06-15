using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class BondsSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bonds",
            "Путы"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1 Thorns Growth for each 2 <cr> drawn this turn",
            "Добавьте 1 Рост Шиопв за каждые 2 <cr> взятые на этом ходу."
        );
        public override SpellType Type => SpellType.Nature;
        
        private int _cardsDrawn;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            for (int i = 0; i < _cardsDrawn / 2; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthThornsSpell)));
            }
        }

        private async void Start()
        {
            GameBoard.PlayerDeck.OnSpellDraw += AddSpellsDrawn;
            GameBoard.Turn.OnPlayerTurnStart += DiscardCardsDrawn;
        }

        private void AddSpellsDrawn(Spell spell) => _cardsDrawn++;
        private void DiscardCardsDrawn() => _cardsDrawn = 0;

        private void OnDestroy()
        {
            GameBoard.PlayerDeck.OnSpellDraw -= AddSpellsDrawn;
            GameBoard.Turn.OnPlayerTurnStart -= DiscardCardsDrawn;
        }
    }
}