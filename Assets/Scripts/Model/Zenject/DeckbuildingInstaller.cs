using Model.Deckbulding;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class DeckbuildingInstaller : MonoInstaller
    {
        [SerializeField] private DeckbuildingBoard _deckbuildingBoard;

        public override void InstallBindings()
        {
            Container.Bind<DeckbuildingBoard>().FromInstance(_deckbuildingBoard).AsSingle();
        }
    }
}