using System;
using System.Linq;
using Model.Travel.Dice;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Model.Travel.Map
{
    [ExecuteAlways]
    public class NodeConnection : MonoBehaviour
    {
        [SerializeField] private bool _moveToEnds;
        [SerializeField] private bool _validateEndsHaveThis;
        [SerializeField] private TravelDieSide _sideRequired;
        [SerializeField] private Node[] _ends;

        public TravelDieSide SideRequired => _sideRequired;
        
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

        public Node GetOtherEnd(Node otherThanThis) => _ends[0].Equals(otherThanThis) ? _ends[1] : _ends[0];

        public bool CompareSide(TravelDieSide side)
        {
            if (_sideRequired == TravelDieSide.Walk)
                return true;
            return _sideRequired == side;
        }

        [ContextMenu("Destroy Connection")]
        private void DestroyConnection()
        {
            foreach (Node end in _ends)
            {
                end.RemoveConnection(this);
            }
            Destroy(gameObject);
        }

        private void OnValidate()
        {
            MoveToEnds();
            ValidateEndsHaveThis();
        }

        private void MoveToEnds()
        {
            if (!_moveToEnds) 
                return;
            
            _moveToEnds = false;
            OnEndsUpdated?.Invoke(_ends);
        }

        private void ValidateEndsHaveThis()
        {
            if (!_validateEndsHaveThis)
                return;
            
            _validateEndsHaveThis = false;
            foreach (var end in _ends)
            {
                if (!end.Connections.Contains(this))
                    end.AddConnection(this);
            }
        }
    }
}