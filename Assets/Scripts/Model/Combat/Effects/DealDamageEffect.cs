using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class DealDamageEffect : Effect
    {
        private readonly Character _target;
        private readonly int _damage;
        private readonly bool _burst;

        public override string[] Sounds => new []{"Effects/Damage1", "Effects/Damage2","Effects/Damage3", "Effects/Damage4"};

        public DealDamageEffect(float dealyAfter, Character target, int damage, bool burst) : base(dealyAfter)
        {
            _target = target;
            _damage = damage;
            _burst = burst;
        }

        public override void Execute()
        {
            _target?.DealDamage((_burst ? 1 : 0) + _damage);
        }
    }
}