using Model.Combat.Characters;

namespace Model.Combat.Effects
{
    public class DealPereodicDamageEffect : Effect
    {
        private readonly Character _target;
        private readonly int _damage;
        private readonly bool _growth;

        public DealPereodicDamageEffect(float dealyAfter, Character target, int damage, bool growth) : base(dealyAfter)
        {
            _target = target;
            _damage = damage;
            _growth = growth;
        }

        public override void Execute()
        {
            if (_target == null)
                return;

            _target.DealPereodicDamage(_growth ? 1 : 0 + _damage);
        }
    }
}