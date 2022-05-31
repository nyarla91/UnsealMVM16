using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class RestoreHealthEffect : Effect
    {
        private readonly Character _target;
        private readonly int _health;
        private readonly bool _growth;

        public RestoreHealthEffect(float dealyAfter, Character target, int health, bool growth) : base(dealyAfter)
        {
            _target = target;
            _health = health;
            _growth = growth;
        }

        public override void Execute()
        {
            if (_target == null)
                return;

            _target.RestoreHealth(_growth ? 1 : 0 + _health);
        }
    }
}