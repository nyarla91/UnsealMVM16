using Model.Global;
using UnityEngine;
using Zenject;

namespace Model.Deckbulding
{
    public class ReturnButton : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private DeckbuildingBoard _deckbuildingBoard;
        
        public void SaveAndReturn()
        {
            _deckbuildingBoard.BuildedDeck.SaveDeck();
            _sceneLoader.LoadTravel();
        }

        public void DiscardAndReturn()
        {
            _sceneLoader.LoadTravel();
        }
    }
}