using DG.Tweening;
using UnityEngine;

namespace View
{
    public class ButtonView : MonoBehaviour
    {
        public void Press()
        {
            transform.DOKill();
            transform.DOLocalMove(new Vector3(0, 0, 0), 0.1f).onComplete += () =>
            {
                transform.DOLocalMove(new Vector3(0, 0.4f, 0), 0.4f);
            };
        }
    }
}