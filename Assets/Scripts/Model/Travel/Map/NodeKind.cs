using Essentials;
using UnityEngine;

namespace Model.Travel.Map
{
    public abstract class NodeKind : Transformer
    {
        public abstract void OnPLayerEnter();
        public virtual void OnPLayerLeave() {}
        public virtual void OnPLayerStartHere() {}
    }
}