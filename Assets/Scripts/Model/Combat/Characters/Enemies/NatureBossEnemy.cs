using Model.Combat.Effects;

namespace Model.Combat.Characters.Enemies
{
    public class NatureBossEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
            int damage = GameBoard.Player.Health - 1;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, GameBoard.Player, damage, false), 0);
        }
    }
}