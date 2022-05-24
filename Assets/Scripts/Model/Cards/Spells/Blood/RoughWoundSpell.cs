using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class RoughWoundSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Rough wound",
            "Рваная ранв"
            );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm>. Trigger all <bl> on it",
            "Наносит 2<dm>. Вызывает срабатывание всего его <bl>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            Character target = await GetTarget<Character>(true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, growth));
            GameBoard.EffectQueue.AddEffect(new TriggerPereodicDamageEffect(0.1f, target));
        }
    }
}