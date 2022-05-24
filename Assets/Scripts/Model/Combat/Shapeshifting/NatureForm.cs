using Model.Cards.Spells;
using Model.Combat.Effects;

namespace Model.Combat.Shapeshifting
{
    public class NatureForm : ShapeshifterForm
    {
        public override void OnEnter()
        {
            GameBoard.EffectQueue.InsertEffect(new AddGrowthEffect(0.05f, GameBoard.PlayerBoard, 2), 0);
            GameBoard.PlayerHand.ForbiddenType = SpellType.Blood;
        }

        public override void OnExit()
        {
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}