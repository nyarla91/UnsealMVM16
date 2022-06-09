using Essentials.Pointers;
using UnityEngine;
using View;
using View.Deckbuilding;
using PointerType = Essentials.Pointers.PointerType;

namespace Presenter.Deckbuilding
{
    public class ReturnButtonPresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private ReturnMessageView _view;

        private void Awake()
        {
            _pointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            _view.FadeIn();
        }
    }
}