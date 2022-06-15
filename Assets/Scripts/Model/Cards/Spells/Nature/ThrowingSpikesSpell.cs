using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class ThrowingSpikesSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Throwing spikes",
            "Метание шипов"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] After you shuffle a deck add 3 Thorns Growth to your dicard pile",
            "[Пассивно:] Когда вы перемешиваете колоду добавьляейте 3 Роста Шипов в сброс"
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerDeck.OnShuffle += OnShuffle;
        }

        private async void OnShuffle()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier * 3; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthThornsSpell)), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerDeck.OnShuffle += OnShuffle;
            base.OnPurge();
        }
    }
}