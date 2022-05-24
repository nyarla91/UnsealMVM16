using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class HitSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Hit",
            "Удар"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 3<dm>",
            "Наносит 3<dm>"
        );
        public override SpellType Type => SpellType.None;

        public override async void OnPlay(bool growth)
        {
            Character target = await GetTarget<Enemy>(true);
            if (target != null)
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 3, growth));
            base.OnPlay(growth);
        }
    }
}