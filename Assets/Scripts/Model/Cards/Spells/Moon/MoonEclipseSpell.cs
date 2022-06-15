using System.Threading.Tasks;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class MoonEclipseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Moon eclipse",
            "Лунный закат"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy. If you have purged a card this turn deal 5<dm> instead",
            "Наносит 2<dm> противнику. Если вы очищали карту в этот ход вместо этого наносит 5<dm>"
        );
        public override SpellType Type => SpellType.Moon;

        private bool _cardsPurged;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            int damage = _cardsPurged ? 5 : 2;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage, burst));
        }

        private async void Start()
        {
            await Task.Delay(100);
            GameBoard.PlayerBoard.OnSpellPurged += OnCardPurged;
            GameBoard.Turn.OnPlayerTurnStart += DiscardCardsPurged;
        }

        private void OnCardPurged(Spell spell) => _cardsPurged = true;
        private void DiscardCardsPurged() => _cardsPurged = false;

        private void OnDestroy()
        {
            GameBoard.PlayerBoard.OnSpellPurged -= OnCardPurged;
            GameBoard.Turn.OnPlayerTurnStart -= DiscardCardsPurged;
        }
    }
}