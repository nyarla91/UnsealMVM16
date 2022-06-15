﻿using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class RestoreHealthEffect : Effect
    {
        private readonly Character _target;
        private readonly int _health;
        private readonly bool _burst;

        public RestoreHealthEffect(float dealyAfter, Character target, int health, bool burst) : base(dealyAfter)
        {
            _target = target;
            _health = health;
            _burst = burst;
        }

        public override void Execute()
        {
            _target?.RestoreHealth((_burst ? 1 : 0) + _health);
        }
    }
}