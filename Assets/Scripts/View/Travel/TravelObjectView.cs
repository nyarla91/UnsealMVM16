using DG.Tweening;
using Essentials;
using UnityEngine;

namespace View.Travel
{
    public class TravelObjectView : Transformer
    {
        [SerializeField] private GameObject _graphics;
        [SerializeField] private float _invisibleY;

        public void Show()
        {
            transform.DOComplete();
            _graphics.SetActive(true);
            transform.DOLocalMove(Vector3.zero, 0.5f);
        }

        public void Hide()
        {
            transform.DOComplete();
            transform.DOLocalMove(Vector3.up * _invisibleY, 0.5f).onComplete += () => _graphics.SetActive(false);
        }
        
    }
}