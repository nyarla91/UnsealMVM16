using Model.Combat.Characters.Enemies;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class DeactivateEnemyEffect : Effect
    {
        private readonly Enemy _target;

        public DeactivateEnemyEffect(float dealyAfter, Enemy target) : base(dealyAfter)
        {
            _target = target;
        }

        public override void Execute()
        {
            _target?.OnDeactivation?.Invoke();
        }
    }
}