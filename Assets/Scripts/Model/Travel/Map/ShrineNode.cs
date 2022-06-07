using System;
using Model.Global;
using Model.Global.Save;
using Zenject;

namespace Model.Travel.Map
{
    public class ShrineNode : NodeKind
    {
        [Inject] private ManualSave _manualSave;
        [Inject] private SceneLoader _sceneLoader;

        public event Action OnPlayerEntered;
        public event Action OnPlayerLeft;
        
        public override void OnPLayerEnter()
        {
            _manualSave.Data.NodePosition = transform.position;
            _manualSave.Save();
            OnPlayerEntered?.Invoke();
        }

        public override void OnPLayerStartHere() => OnPlayerEntered?.Invoke();
        public override void OnPLayerLeave() => OnPlayerLeft?.Invoke();
    }
}