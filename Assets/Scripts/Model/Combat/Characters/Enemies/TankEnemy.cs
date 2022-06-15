using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class TankEnemy : Enemy
    {
        [SerializeField] private int _armorToAll;
        
        protected override void AfterActivation()
        {
            foreach (Enemy enemy in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.05f, enemy, _armorToAll, false), 0);
            }
        }
    }
}