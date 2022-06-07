using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Deckbulding
{
    public class SaveAndReturnButton : ReturnButton
    {
        [Inject] private DeckbuildingBoard _deckbuildingBoard;
        
        protected override void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left || _deckbuildingBoard.BuildedDeck.Size < 20)
                return;
            
            _deckbuildingBoard.BuildedDeck.SaveDeck();
            _sceneLoader.LoadTravel();
        }
    }
}