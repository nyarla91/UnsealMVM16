using DG.Tweening;
using Essentials;
using UnityEngine;

namespace View.Combat.Characters
{
    public class DescendingObject : Transformer
    {
        protected virtual Transform MovedObject => transform;

        protected bool _descended = true;

        protected Vector3 TopLocalPosition => MovedObject.localPosition.WithY(20);
        protected Vector3 GoundLocalPosition => MovedObject.localPosition.WithY(0);
        
        public void Descend()
        {
            if (_descended)
                return;

            _descended = true;
            MovedObject.DOComplete();
            MovedObject.localPosition = TopLocalPosition;
            MovedObject.DOLocalMove(GoundLocalPosition, 0.5f);
        }

        public void Ascend(bool destroy)
        {
            if (!_descended)
                return;

            _descended = false;
            MovedObject.DOComplete();
            MovedObject.localPosition = GoundLocalPosition;
            MovedObject.DOLocalMove(TopLocalPosition, 0.5f).onComplete += () => { if (destroy) Destroy(gameObject); };
        }

        protected void AscendImmediately()
        {
            if (!_descended)
                return;
            
            _descended = false;
            MovedObject.DOComplete();
            MovedObject.localPosition = TopLocalPosition;
        }
    }
}