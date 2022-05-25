using UnityEngine;

namespace Essentials
{
    public abstract class Transformer : MonoBehaviour
    {
        private Transform _transform;
        public new Transform transform => _transform ??= ((Component) this).transform;
    }
}