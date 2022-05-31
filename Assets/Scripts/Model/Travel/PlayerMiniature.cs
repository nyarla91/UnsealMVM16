using System;
using Essentials;
using Model.Global;
using Model.Travel.Map;
using UnityEngine;
using Zenject;

namespace Model.Travel
{
    public class PlayerMiniature : Transformer
    {
        [SerializeField] private Node _startingNode;
        
        private Node _currentNode;

        public Node CurrentNode => _currentNode;

        private GlobalTravelState _globalTravelState;
        
        [Inject]
        private void Construct(GlobalTravelState globalTravelState)
        {
            _globalTravelState = globalTravelState;
        }

        public void MoveToNode(Node destination)
        {
            _currentNode = destination;
            _globalTravelState.CurrentNodePosition = destination.transform.position;
        }

        public void MoveToNodeInstantly(Node destination)
        {
            MoveToNode(destination);
            transform.position = destination.transform.position;
        }

        private void Start()
        {
            Collider[] overlap = Physics.OverlapSphere(_globalTravelState.CurrentNodePosition, 0.01f);
            if (overlap == null || overlap.Length == 0 || !overlap[0].TryGetComponent(out Node node))
            {
                MoveToNodeInstantly(_startingNode);
                return;
            }
            MoveToNodeInstantly(node);
            
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 7;
            transform.position = Vector3.Lerp(transform.position, _currentNode.transform.position,
                MovementSpeed * Time.fixedDeltaTime);
        }
    }
}