using Model.Combat.Characters.Enemies;

namespace Model.Combat.Effects
{
    public class ActivateEnemyEffect : Effect
    {
        private readonly Enemy _target;

        public ActivateEnemyEffect(float dealyAfter, Enemy target) : base(dealyAfter)
        {
            _target = target;
        }

        public override void Execute()
        {
            _target?.OnActivation?.Invoke();
        }
    }
}