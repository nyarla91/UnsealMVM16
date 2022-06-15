using System;
using DG.Tweening;
using Essentials;
using Model.Global;
using Model.Global.Save;
using Model.Travel.Map;
using UnityEngine;
using Zenject;

namespace Model.Travel
{
    public class PlayerMiniature : Transformer
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Node _startingNode;
        
        private Node _currentNode;

        public Node CurrentNode => _currentNode;

        private GlobalTravelState _globalTravelState;
        private ManualSave _manualSave;
        public event Action OnMovedInstantly;

        [Inject]
        private void Construct(GlobalTravelState globalTravelState, ManualSave manualSave)
        {
            _globalTravelState = globalTravelState;
            _manualSave = manualSave;
        }

        public void MoveToNode(Node destination)
        {
            _currentNode = destination;
            _globalTravelState.CurrentNodePosition = destination.transform.position;
            transform.DOComplete();
            transform.DOLocalJump(destination.transform.position, 4, 1, 0.5f);
        }

        public void MoveToNodeInstantly(Node destination)
        {
            transform.DOComplete();
            MoveToNode(destination);
            destination.OnPlayerStartHere();
            transform.position = destination.transform.position;
            OnMovedInstantly?.Invoke();
        }

        private void Start()
        {
            if (TryLoadNodeFromPosition(out Node loadedNode, _globalTravelState.CurrentNodePosition))
                MoveToNodeInstantly(loadedNode);
            else if (TryLoadNodeFromPosition(out loadedNode, _manualSave.Data.NodePosition))
                MoveToNodeInstantly(loadedNode);
            else
                MoveToNodeInstantly(_startingNode);
            _collider.enabled = true;
        }

        private bool TryLoadNodeFromPosition(out Node node, Vector3 position)
        {
            if (position == Vector3.down)
            {
                node = null;
                return false;
            }
            Collider[] overlap = Physics.OverlapSphere(position, 0.01f);
            if (overlap == null || overlap.Length == 0 || !overlap[0].TryGetComponent(out Node overlapNode))
            {
                node = null;
                return false;
            }
            node = overlapNode;
            return true;
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 7;
        }
    }
}