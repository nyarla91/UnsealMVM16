using Essentials;
using Model.Cards.Spells;
using Model.Combat.Characters;
using Model.Combat.Effects;

namespace Model.Combat.Shapeshifting
{
    public class BloodForm : Form
    {
        public override void Enter()
        {
            base.Enter();
            GameBoard.Player.OnLoseHealth += OnLoseHealth;
            GameBoard.PlayerHand.ForbiddenType = SpellType.Nature;
        }

        private void OnLoseHealth(int healthLost)
        {
            Enemy target = GameBoard.EnemyPool.ActiveEnemies.PickRandomElement();
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.05f, GameBoard.Player, 1, false), 0);
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.05f, target, healthLost, false), 1);
        }

        public override void Exit()
        {
            base.Exit();
            GameBoard.Player.OnLoseHealth -= OnLoseHealth;
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}