using System;
using Essentials;
using Essentials.Pointers;
using Model.Travel;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Presenter.Travel.Camera
{
    public class TravelCamera : Transformer
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _moveOrigin;
        [SerializeField] private Transform _rotationOrigin;
        [SerializeField] private Transform _cameraObjects;
        
        [Inject] private PlayerMiniature _player;
        [Inject] private Pause _pause;

        private bool _mapMode;
        private bool _drag;
        private float _targetTilt = -20;
        private float _targetY = 25;
        private float _targetCameraObjectsY;

        public bool MapMode => _mapMode;

        public event Action OnCameraModeSwitch;

        private void Awake()
        {
            PointerControls.Actions.Mouse.MiddleClick.performed += SwitchCameraMode;
            PointerControls.Actions.Mouse.LeftMouse.started += StartDrag;
            PointerControls.Actions.Mouse.LeftMouse.canceled += EndDrag;
            _player.OnMovedInstantly += MoveToPlayer;
        }

        private void MoveToPlayer()
        {
            transform.position = _player.transform.position;
        }

        private void StartDrag(InputAction.CallbackContext callback) => _drag = true;
        private void EndDrag(InputAction.CallbackContext callback) => _drag = false;
        private void FixedUpdate()
        {
            const float MovementSpeed = 5;
            float t = Time.deltaTime * MovementSpeed;
            
            Transform targetOrigin = _mapMode ? _moveOrigin : _player.transform;
            transform.position = Vector3.Lerp(transform.position, targetOrigin.position.WithY(0), t);
            
            float xRotation = Mathf.LerpAngle(_rotationOrigin.localRotation.eulerAngles.x, _targetTilt, t);
            _rotationOrigin.localRotation = Quaternion.Euler(xRotation, 0, 0);
            
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, _camera.localPosition.WithY(_targetY), t);
            
            float cameraObectsY = Mathf.Lerp(_cameraObjects.localPosition.y, _targetCameraObjectsY, t);
            _cameraObjects.localPosition = _cameraObjects.localPosition.WithY(cameraObectsY);
            
            if (_mapMode && _drag)
            {
                Vector2 delta = PointerControls.Actions.Mouse.Delta.ReadValue<Vector2>();
                _moveOrigin.transform.position += -delta.XYtoXZ() * Time.fixedDeltaTime * 20;
            }
        }

        public void EnableTravelMode()
        {
            if (_mapMode)
                SwitchCameraMode(new InputAction.CallbackContext());
        }

        public void EnableMapMode()
        {
            if (!_mapMode)
                SwitchCameraMode(new InputAction.CallbackContext());
        }

        private void SwitchCameraMode(InputAction.CallbackContext callbackContext)
        {
            if (_pause.IsPaused)
                return;
            
            _mapMode = !_mapMode;
            OnCameraModeSwitch?.Invoke();
            
            _targetTilt = _mapMode ? 0 : -20;
            _targetY = _mapMode ? 50 : 25;
            _targetCameraObjectsY = _mapMode ? -5 : 0;
            if (_mapMode)
            {
                _moveOrigin.position = _player.transform.position;
            }
        }

        private void OnDestroy()
        {
            PointerControls.Actions.Mouse.MiddleClick.performed -= SwitchCameraMode;
        }
    }
}