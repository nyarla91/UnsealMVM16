using Essentials;
using Essentials.Pointers;
using Model.Travel;
using UnityEngine;
using Zenject;

namespace Presenter.Travel.Camera
{
    public class TravelCamera : Transformer
    {
        [SerializeField] private Transform _moveOrigin;
        
        [Inject] private PlayerMiniature _player;

        private bool _mapMode;
        
        private void FixedUpdate()
        {
            const float MovementSpeed = 3;
            Transform targetOrigin = _mapMode ? _moveOrigin : _player.transform;
            transform.position = Vector3.Lerp(transform.position, targetOrigin.position, Time.fixedDeltaTime * MovementSpeed);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _mapMode = !_mapMode;
                if (_mapMode)
                {
                    _moveOrigin.position = _player.transform.position;
                }
            }

            if (_mapMode && Input.GetMouseButton(0))
            {
                Vector2 delta = PointerControls.Actions.Mouse.Delta.ReadValue<Vector2>();
                _moveOrigin.transform.position += -delta.XYtoXZ() * Time.fixedDeltaTime;
            }
        }
    }
}