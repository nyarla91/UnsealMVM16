using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class SunBossEnemy : Enemy
    {
        [SerializeField] private int _damageOnNotConsumeBurst;
        
        protected override void BeforeActivation()
        {
            base.BeforeActivation();
            DamageBonus = 0;
        }

        protected override void AfterActivation()
        {
            base.AfterActivation();
            if (!GameBoard.PlayerBoard.TrySpendBurst())
            {
                DamageBonus = _damageOnNotConsumeBurst;
            }
        }
    }
}