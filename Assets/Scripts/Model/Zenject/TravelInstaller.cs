using Model.Travel;
using Model.Travel.Dice;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class TravelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMiniature _playerMiniature;
        [SerializeField] private TravelDicePool dicePool;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMiniature>().FromInstance(_playerMiniature).AsSingle();
            Container.Bind<TravelDicePool>().FromInstance(dicePool).AsSingle();
        }
    }
}