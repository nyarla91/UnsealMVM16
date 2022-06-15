using Essentials;
using UnityEngine;

namespace Model.Travel.Map
{
    public abstract class NodeKind : Transformer
    {
        [SerializeField] private bool _spendDie = true;

        public bool SpendDie => _spendDie;
        
        public abstract void OnPLayerEnter();
        public virtual void OnPLayerLeave() {}
        public virtual void OnPLayerStartHere() {}
    }
}