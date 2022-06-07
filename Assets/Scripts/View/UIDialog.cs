using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class UIDialog : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private List<GameObject> _objectsToSwitch;
        
        protected CanvasGroup CanvasGroup => _canvasGroup;

        public virtual void FadeIn()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1, 0.2f);
            foreach (GameObject switched in _objectsToSwitch)
                switched.SetActive(true);
        }

        public virtual void FadeOut()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(0, 0.2f);
            foreach (GameObject switched in _objectsToSwitch)
                switched.SetActive(false);
        }
    }
}