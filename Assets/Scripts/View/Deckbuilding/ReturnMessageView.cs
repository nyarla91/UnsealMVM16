using Essentials.Pointers;
using Model.Deckbulding;
using UnityEngine;
using Zenject;

namespace View.Deckbuilding
{
    public class ReturnMessageView : UIDialog
    {
        [SerializeField] private CanvasGroup _saveButton;

        [Inject] private DeckbuildingBoard _deckbuildingBoard;
        
        public override void FadeIn()
        {
            base.FadeIn();
            PointerCaster.Instance.ActivateMask(1);
            bool saveAllowed = _deckbuildingBoard.BuildedDeck.SaveAllowed;
            _saveButton.alpha = saveAllowed ? 1 : 0.4f;
            _saveButton.blocksRaycasts = saveAllowed;
        }

        public override void FadeOut()
        {
            base.FadeOut();
            PointerCaster.Instance.ActivateMask(0);
        }
    }
}