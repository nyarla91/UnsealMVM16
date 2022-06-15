using Model.Combat.Characters;

namespace Model.Combat.Effects
{
    public class AddArmorEffect : Effect
    {
        private readonly Character _target;
        private readonly int _armor;
        private readonly bool _burst;

        public AddArmorEffect(float dealyAfter, Character target, int armor, bool burst) : base(dealyAfter)
        {
            _target = target;
            _armor = armor;
            _burst = burst;
        }

        public override void Execute()
        {
            if (_target == null)
                return;
            
            _target.AddArmor((_burst ? 1 : 0) + _armor);
        }
    }
}