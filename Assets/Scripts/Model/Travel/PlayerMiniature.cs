using System;
using Essentials;
using Model.Travel.Map;
using UnityEngine;

namespace Model.Travel
{
    public class PlayerMiniature : Transformer
    {
        [SerializeField] private Node _startingNode;
        
        private Node _currentNode;

        public Node CurrentNode => _currentNode;

        public void MoveToNode(Node destination)
        {
            _currentNode = destination;
        }

        private void Start()
        {
            MoveToNode(_startingNode);
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 7;
            transform.position = Vector3.Lerp(transform.position, _currentNode.transform.position,
                MovementSpeed * Time.fixedDeltaTime);
        }
    }
}