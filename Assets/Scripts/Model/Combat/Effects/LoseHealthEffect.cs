using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class LoseHealthEffect : Effect
    {
        private readonly Character _target;
        private readonly int _health;
        public override string[] Sounds => new []{"Effects/Damage1", "Effects/Damage2","Effects/Damage3", "Effects/Damage4"};

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