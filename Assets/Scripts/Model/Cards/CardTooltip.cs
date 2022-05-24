using System;
using Essentials;
using Essentials.Pointers;
using Model.Cards.Spells;
using Presenter.Cards;
using UnityEngine;

namespace Model.Cards
{
    public class CardTooltip : Transformer
    {
        [SerializeField] private CardPresenter _presenter;
        
        private Vector2 _screenOffset;
        private Spell _target;

        public void Show(Spell target, Vector3 screenOffset)
        {
            _presenter.Show();
            GetComponent<Spell>()?.SelfDestruct();
            gameObject.AddComponent(target.GetType());
            _target = target;
            _screenOffset = screenOffset;
        }

        public void Hide(Spell target)
        {
            if (_target != target)
                return;
            
            GetComponent<Spell>()?.SelfDestruct();
            _target = null;
            _presenter.Hide();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 offset = new Vector2(_screenOffset.x * Screen.width, _screenOffset.y * Screen.height);
            print(offset);
            Vector2 mousePosition = PointerControls.Actions.Mouse.Position.ReadValue<Vector2>() + offset;
            Ray ray = CameraProperties.Instance.Main.ScreenPointToRay(mousePosition);
            LayerMask mask = LayerMask.GetMask("UI");
            
            if (!Physics.Raycast(ray, out RaycastHit raycast, 500, mask))
                return;
            
            transform.position = raycast.point;
        }
    }
}