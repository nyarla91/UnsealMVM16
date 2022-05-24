using Model.Combat.GameAreas;

namespace Model.Combat.Effects
{
    public class AddGrowthEffect : Effect
    {
        private readonly PlayerBoard _playerBoard;
        private readonly int _growth;

        public AddGrowthEffect(float dealyAfter, PlayerBoard playerBoard, int growth) : base(dealyAfter)
        {
            _playerBoard = playerBoard;
            _growth = growth;
        }

        public override void Execute()
        {
            _playerBoard.AddGrowth(_growth);
        }
    }
}