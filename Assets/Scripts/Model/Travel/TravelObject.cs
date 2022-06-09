using System;
using Essentials;
using Model.Global.Save;
using Presenter.Travel.Camera;
using UnityEngine;
using Zenject;

namespace Model.Travel
{
    public class TravelObject : MonoBehaviour
    {
        [SerializeField] private bool _decoration;

        
        private bool _visibleByPlayer;
        private bool _show = true;

        private bool Explored => _permanentSave.Data.Map.Contains(gameObject.name);

        public event Action OnShow;
        public event Action OnHide;

        private PermanentSave _permanentSave;
        private TravelCamera _travelCamera;

        [Inject]
        public void Construct(PermanentSave permanentSave, TravelCamera travelCamera)
        {
            _permanentSave = permanentSave;
            _travelCamera = travelCamera;
            _travelCamera.OnCameraModeSwitch += UpdateVisibility;
        }

        private void Start()
        {
            UpdateVisibility();
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerMiniature irrelevant))
                return;
            await MiscEssentials.WaitForCondition(this, travleObject => _permanentSave != null, 50);
            _visibleByPlayer = true;
            UpdateVisibility();
            if (Explored || _decoration)
                return;
            _permanentSave.Data.Map.Add(gameObject.name);
            _permanentSave.Save();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out PlayerMiniature irrelevant))
                return;
            _visibleByPlayer = false;
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            bool show = _decoration ? (_visibleByPlayer && !_travelCamera.MapMode) : (_visibleByPlayer || Explored);

            if (_show == show)
                return;
            
            if (show)
                OnShow?.Invoke();
            else
                OnHide?.Invoke();
            _show = show;
        }
    }
}