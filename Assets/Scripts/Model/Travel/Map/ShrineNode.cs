using System;
using Essentials.Pointers;
using Model.Global;
using Model.Global.Save;
using Presenter.Travel.Camera;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Travel.Map
{
    public class ShrineNode : NodeKind
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private Node _node;
        
        [Inject] private ManualSave _manualSave;
        [Inject] private GlobalTravelState _travelState;
        [Inject] private PermanentSave _permanentSave;
        [Inject] private PlayerMiniature _playerMiniature;
        [Inject] private TravelCamera _travelCamera;
        
        private bool _fastTravelActive = true;

        private bool Locked =>!_permanentSave.Data.ShrinesUnlocked.Contains(gameObject.name);

        private bool FastTravelActive
        {
            get => _fastTravelActive;
            set
            {
                if (_fastTravelActive == value)
                    return;
                
                OnFastTravelSwitched?.Invoke(value);
                _fastTravelActive = value;
            }
        }

        public event Action<bool> OnFastTravelSwitched;
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeft;

        private void Awake()
        {
            _pointerTarget.OnClick += UseFastTravel;
        }

        private void UseFastTravel(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Right || !FastTravelActive)
                return;
            
            _travelState.Reset();
            _playerMiniature.MoveToNodeInstantly(_node);
            _travelCamera.EnableTravelMode();
        }

        private void Start()
        {
            if (Locked)
            {
                FastTravelActive = false;
            }
        }

        public override void OnPLayerEnter()
        {
            _travelState.Reset();
            if (Locked)
            {
                _permanentSave.Data.ShrinesUnlocked.Add(gameObject.name);
                _permanentSave.Save();
                FastTravelActive = true;
            }
            _manualSave.Data.NodePosition = transform.position;
            _manualSave.Save();
            if (_permanentSave.Data.CardsUnlocked.Count >= 12)
                OnPlayerEntered?.Invoke();
        }

        public override void OnPLayerStartHere() => OnPlayerEntered?.Invoke();
        public override void OnPLayerLeave() => OnPlayerLeft?.Invoke();
    }
}