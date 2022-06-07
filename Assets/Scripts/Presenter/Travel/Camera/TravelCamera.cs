using DG.Tweening;
using Essentials;
using Essentials.Pointers;
using Model.Travel;
using UnityEngine;
using Zenject;

namespace Presenter.Travel.Camera
{
    public class TravelCamera : Transformer
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _moveOrigin;
        [SerializeField] private Transform _rotationOrigin;
        [SerializeField] private Transform _cameraObjects;
        
        [Inject] private PlayerMiniature _player;

        private bool _mapMode;
        private float _targetTilt = -20;
        private float _targetFOV = 60;
        private float _targetCameraObjectsY = 0;
        
        private void FixedUpdate()
        {
            const float MovementSpeed = 5;
            float t = Time.deltaTime * MovementSpeed;
            Transform targetOrigin = _mapMode ? _moveOrigin : _player.transform;
            transform.position = Vector3.Lerp(transform.position, targetOrigin.position, t);
            float xRotation = Mathf.LerpAngle(_rotationOrigin.localRotation.eulerAngles.x, _targetTilt, t);
            _rotationOrigin.localRotation = Quaternion.Euler(xRotation, 0, 0);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFOV, t);
            float cameraObectsY = Mathf.Lerp(_cameraObjects.localPosition.y, _targetCameraObjectsY, t);
            _cameraObjects.localPosition = _cameraObjects.localPosition.WithY(cameraObectsY);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _mapMode = !_mapMode;
                _targetTilt = _mapMode ? 0 : -20;
                _targetFOV = _mapMode ? 90 : 60;
                _targetCameraObjectsY = _mapMode ? -5 : 0;
                if (_mapMode)
                {
                    _moveOrigin.position = _player.transform.position;
                }
            }

            if (_mapMode && Input.GetMouseButton(0))
            {
                Vector2 delta = PointerControls.Actions.Mouse.Delta.ReadValue<Vector2>();
                _moveOrigin.transform.position += -delta.XYtoXZ() * Time.fixedDeltaTime * 2;
            }
        }
    }
}