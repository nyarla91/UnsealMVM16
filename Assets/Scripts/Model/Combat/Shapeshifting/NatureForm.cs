using Model.Cards.Spells;
using Model.Combat.Effects;

namespace Model.Combat.Shapeshifting
{
    public class NatureForm : Form
    {
        public override void Enter()
        {
            base.Enter();
            GameBoard.EffectQueue.AddEffect(new AddGrowthEffect(0.05f, GameBoard.PlayerBoard, 2), 0);
            GameBoard.PlayerHand.ForbiddenType = SpellType.Blood;
        }

        public override void Exit()
        {
            base.Exit();
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}