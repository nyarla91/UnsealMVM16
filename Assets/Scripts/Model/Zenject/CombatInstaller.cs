using Model.Cards;
using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class CombatInstaller : MonoInstaller
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private Canvas _canvas;
        
        public override void InstallBindings()
        {
            Container.Bind<GameBoard>().FromInstance(_gameBoard).AsSingle();
            Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
        }
    }
}