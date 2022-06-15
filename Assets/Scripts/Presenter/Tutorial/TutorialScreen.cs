using DG.Tweening;
using Model.Global.Save;
using UnityEngine;
using View;
using Zenject;

namespace Presenter.Tutorial
{
    public class TutorialScreen : UIDialog
    {
        [Inject] private Pause _pause;
        [Inject] private PermanentSave _permanentSave;

        protected PermanentSave PermanentSave => _permanentSave;
        
        protected void Show()
        {
            if (_permanentSave.Data.TutorialsWatched.Contains(gameObject.name))
                return;
            _pause.Begin(this);
            FadeIn();
        }

        public void Hide()
        {
            _pause.End(this);
            _permanentSave.Data.TutorialsWatched.Add(gameObject.name);
            FadeOut();
        }
    }
}