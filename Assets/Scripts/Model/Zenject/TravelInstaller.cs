using Model.Travel;
using Model.Travel.Dice;
using Presenter.Travel.Camera;
using UnityEngine;
using View.Cards;
using Zenject;

namespace Model.Zenject
{
    public class TravelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMiniature _playerMiniature;
        [SerializeField] private TravelDicePool _dicePool;
        [SerializeField] private TravelCamera _travelCamera;
        [SerializeField] private AbilitiyTooltip _abilitiyTooltip;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMiniature>().FromInstance(_playerMiniature).AsSingle();
            Container.Bind<TravelDicePool>().FromInstance(_dicePool).AsSingle();
            Container.Bind<TravelCamera>().FromInstance(_travelCamera).AsSingle();
            Container.Bind<AbilitiyTooltip>().FromInstance(_abilitiyTooltip).AsSingle();
        }
    }
}