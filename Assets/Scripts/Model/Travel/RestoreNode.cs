using Model.Travel.Dice;
using Model.Travel.Map;
using UnityEngine;
using Zenject;

namespace Model.Travel
{
    public class RestoreNode : NodeKind
    {
        [Inject] private TravelDicePool _dicePool;
        
        public override void OnPLayerEnter()
        {
            _dicePool.ReadyRandomDie();
        }
    }
}