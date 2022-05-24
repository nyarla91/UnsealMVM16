using Model.Combat.Characters;

namespace Model.Combat.Effects
{
    public class ClearArmorEffect : Effect
    {
        private readonly Character _target;

        public ClearArmorEffect(float dealyAfter, Character target) : base(dealyAfter)
        {
            _target = target;
        }

        public override void Execute()
        {
            _target.ClearArmor();
        }
    }
}