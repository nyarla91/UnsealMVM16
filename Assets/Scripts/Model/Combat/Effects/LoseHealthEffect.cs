using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class LoseHealthEffect : Effect
    {
        private readonly Character _target;
        private readonly int _health;

        public LoseHealthEffect(float dealyAfter, Character target, int health) : base(dealyAfter)
        {
            _target = target;
            _health = health;
        }

        public override void Execute()
        {
            _target.LoseHealth(_health);
        }
    }
}