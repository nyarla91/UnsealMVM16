using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class GatherStrengthSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Gather strength",
            "Из последних сил"
        );
        public override LocalizedString Description => new LocalizedString(
            "Apply 4<bl> to an enemy.\nIf you have 15 <hp> or less apply 9<bl> instead",
            "Накладывает 4<bl> на противника.\nЕсли у вас 15 <hp> или меньше, накладывает 9<bl> вместо этого"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            int bleed = GameBoard.Player.Health <= 15 ? 9 : 4;
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, bleed, Burst), 0);
        }
    }
}