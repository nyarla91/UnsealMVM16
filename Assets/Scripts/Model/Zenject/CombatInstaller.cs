using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class CombatInstaller : MonoInstaller
    {
        [SerializeField] private GameBoard _gameBoard;
        
        public override void InstallBindings()
        {
            Container.Bind<GameBoard>().FromInstance(_gameBoard).AsSingle();
        }
    }
}