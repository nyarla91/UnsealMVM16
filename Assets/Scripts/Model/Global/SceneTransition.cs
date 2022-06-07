using DG.Tweening;
using Essentials;
using UnityEngine;
using Zenject;

namespace Model.Global
{
    public class SceneTransition : Transformer
    {
        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            sceneLoader.Transition = this;
            FadeOut();
        }
        
        public void FadeIn()
        {
            RectTransform.DOKill();
            RectTransform.anchoredPosition = new Vector2(-3840, 0);
            RectTransform.DOAnchorPos(new Vector2(0, 0), SceneLoader.TransitionTime);
        }

        public void FadeOut()
        {
            RectTransform.DOKill();
            RectTransform.anchoredPosition = new Vector2(0, 0);
            RectTransform.DOAnchorPos(new Vector2(3840, 0), SceneLoader.TransitionTime);
        }
    }
}