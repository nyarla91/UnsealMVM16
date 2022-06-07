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
            "Deal 2<dm>. Trigger all <bl> on the target",
            "Наносит 2<dm>. Вызывает срабатывание всего <bl> цели"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Character target = await GetTarget<Character>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, burst));
            GameBoard.EffectQueue.AddEffect(new TriggerPereodicDamageEffect(0.1f, target));
        }
    }
}