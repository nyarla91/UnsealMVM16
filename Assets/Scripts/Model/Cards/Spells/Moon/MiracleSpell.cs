using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class MiracleSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Miracle",
            "Чудо"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 1<dm> to an enemy for each <cr> played this turn (including this)",
            "Наносит выбранному противнику 1<dm> за каждую <cr>, разыгранную на этом ходу (включая эту)"
        );
        public override SpellType Type => SpellType.Moon;
        public override async void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            Enemy target = await GetTarget<Enemy>(true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, GameBoard.Turn.CardsPlayedThisTurn, Growth));
        }
    }
}