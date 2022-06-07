using DG.Tweening;
using Presenter.Travel.Map;
using UnityEngine;

namespace View.Travel
{
    public class EditDeckButton : UIDialog
    {
        private ShrineNodePresenter _presenter;
        
        public void Show(ShrineNodePresenter presenter)
        {
            _presenter = presenter;
            FadeIn();
        }

        public void Hide()
        {
            _presenter = null;
            FadeOut();
        }

        public void EditDeck() => _presenter.EditDeck();
    }
}