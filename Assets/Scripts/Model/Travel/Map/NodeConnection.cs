using System;
using System.Linq;
using UnityEngine;

namespace Model.Travel.Map
{
    [ExecuteAlways]
    public class NodeConnection : MonoBehaviour
    {
        [SerializeField] private bool _moveToEnds;
        [SerializeField] private Node[] _ends;

        public event Action<Node[]> OnEndsUpdated; 
        
        public void Init(Node[] ends)
        {
            if (ends.Length != 2)
                throw new Exception("Node Connection can only have 2 ends");

            _ends = ends;
            foreach (Node end in ends)
            {
                end.AddConnection(this);
            }
            OnEndsUpdated?.Invoke(_ends);
        }

        public Node GetOtherNode(Node otherThanThis) => _ends[0].Equals(otherThanThis) ? _ends[1] : _ends[0];

        private void OnDestroy()
        {
            foreach (Node end in _ends)
            {
                end.RemoveConnection(this);
            }
        }

        private void OnValidate()
        {
            if (!_moveToEnds)
                return;
            _moveToEnds = false;
            OnEndsUpdated?.Invoke(_ends);
        }
    }
}