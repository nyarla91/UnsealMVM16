using Model.Travel;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class TravelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMiniature _playerMiniature;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMiniature>().FromInstance(_playerMiniature).AsSingle();
        }
    }
}