using Model.Combat.GameAreas;

namespace Model.Combat.Effects
{
    public class AddBurstEffect : Effect
    {
        private readonly PlayerBoard _playerBoard;
        private readonly int _burst;

        public AddBurstEffect(float dealyAfter, PlayerBoard playerBoard, int burst) : base(dealyAfter)
        {
            _playerBoard = playerBoard;
            _burst = burst;
        }

        public override void Execute()
        {
            _playerBoard.AddGrowth(_burst);
        }
    }
}