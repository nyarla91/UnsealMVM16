using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class TriskelionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Triskelion",
            "Трискелион"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1 Thorns Growth to your discard pile. If your have drawn a growth card this turn add another one on top of your deck",
            "Добавьте 1 Рост Шиопв в ваш сброс. Если вы брали Рост в этом ходу, добавьте ещё один на верх колоды."
        );
        public override SpellType Type => SpellType.Nature;
        
        private bool _growthDrawn;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                GameBoard.PlayerDiscardPile, typeof(GrowthThornsSpell)));
            if (!_growthDrawn)
                return;
            GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDeck>(0.1f,
                GameBoard.PlayerDeck, typeof(GrowthThornsSpell)));
        }

        private async void Start()
        {
            GameBoard.PlayerDeck.OnSpellDraw += AddSpellsDrawn;
            GameBoard.Turn.OnPlayerTurnStart += DiscardCardsDrawn;
        }

        private void AddSpellsDrawn(Spell spell) => _growthDrawn = _growthDrawn || spell is GrowthSpell;
        private void DiscardCardsDrawn() => _growthDrawn = false;

        private void OnDestroy()
        {
            GameBoard.PlayerDeck.OnSpellDraw -= AddSpellsDrawn;
            GameBoard.Turn.OnPlayerTurnStart -= DiscardCardsDrawn;
        }
    }
}