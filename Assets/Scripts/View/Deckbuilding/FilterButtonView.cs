using DG.Tweening;
using Essentials;
using UnityEngine;

namespace View.Deckbuilding
{
    public class FilterButtonView : Transformer
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _pressedMaterial;
        [SerializeField] private Material _releasedMaterial;
        
        public void Press()
        {
            transform.DOKill();
            transform.DOLocalMove(new Vector3(0, -1, 0), 0.2f);
            _meshRenderer.material = _pressedMaterial;
        }

        public void Release()
        {
            transform.DOKill();
            transform.DOLocalMove(Vector3.zero, 0.2f);
            _meshRenderer.material = _releasedMaterial;
        }
    }
}