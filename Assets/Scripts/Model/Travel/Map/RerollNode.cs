using Model.Travel.Dice;
using Zenject;

namespace Model.Travel.Map
{
    public class RerollNode : NodeKind
    {
        [Inject] private TravelDicePool _dicePool;
        
        public override void OnPLayerEnter()
        {
            _dicePool.RerollAll();
        }
    }
}