using Model.Combat.GameAreas;

namespace Model.Combat.Effects.Inner
{
    public class StartTurnEffect : Effect
    {
        private readonly Turn _turn;

        public StartTurnEffect(Turn turn, float dealyAfter) : base(dealyAfter)
        {
            _turn = turn;
        }

        public override void Execute()
        {
            _turn.StartTurn();
        }
    }
}