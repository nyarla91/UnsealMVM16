using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class GarroteSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Garrote",
            "Гаррота"
        );
        public override LocalizedString Description => new LocalizedString(
            "Trigger all <bl> on enemy twice",
            "Дважды вызывает срабатываение всего <bl> противника"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new TriggerPereodicDamageEffect(0.1f, target), 0);
            GameBoard.EffectQueue.AddEffect(new TriggerPereodicDamageEffect(0.1f, target), 1);
        }
    }
}