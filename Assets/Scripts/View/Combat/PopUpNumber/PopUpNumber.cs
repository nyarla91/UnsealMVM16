using DG.Tweening;
using Essentials;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View.Combat.PopUpNumber
{
    public class PopUpNumber : Transformer
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Vector2 _magnitude;
        [SerializeField] private float _jumoPower;

        public void Init(int value)
        {
            _textMesh.text = value.ToString();
            Vector2 targetAnchorPosition = new Vector2(
                Random.Range(_magnitude.x, -_magnitude.x), Random.Range(_magnitude.y, -_magnitude.y));
            targetAnchorPosition += RectTransform.anchoredPosition;
            RectTransform.DOJumpAnchorPos(targetAnchorPosition, _jumoPower, 1, 1.2f);
            RectTransform.DOScale(Vector3.zero, 1.2f).OnComplete(() => { Destroy(gameObject); });
    }
    }
}