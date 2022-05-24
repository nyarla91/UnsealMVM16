using Essentials;
using Model.Cards.Spells;
using Model.Combat.Characters;
using Model.Combat.Effects;

namespace Model.Combat.Shapeshifting
{
    public class BloodForm : ShapeshifterForm
    {
        public override void OnEnter()
        {
            GameBoard.Player.OnLoseHealth += OnLoseHealth;
            GameBoard.PlayerHand.ForbiddenType = SpellType.Nature;
        }

        private void OnLoseHealth(int healthLost)
        {
            Enemy target = GameBoard.Enemies.PickRandomElement();
            GameBoard.EffectQueue.InsertEffect(new RestoreHealthEffect(0.05f, GameBoard.Player, 1, false), 0);
            GameBoard.EffectQueue.InsertEffect(new DealPereodicDamageEffect(0.05f, target, healthLost, false), 1);
        }

        public override void OnExit()
        {
            GameBoard.Player.OnLoseHealth -= OnLoseHealth;
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}