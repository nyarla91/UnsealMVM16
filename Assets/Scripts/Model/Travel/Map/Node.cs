using System;
using System.Collections.Generic;
using System.Linq;
using Essentials;
using Essentials.Pointers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Travel.Map
{
    public class Node : Transformer
    {
        [SerializeField] private bool _createConnection;
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private NodeConnection _connectionPrefab;
        [SerializeField] private List<NodeConnection> _connections;

        [Inject] private PlayerMiniature _player;
        
        public void AddConnection(NodeConnection connection)
        {
            _connections.Add(connection);
        }

        public void RemoveConnection(NodeConnection connection)
        {
            _connections.Remove(connection);
        }

        public void CreateConnection(Node other)
        {
            NodeConnection connectionPrefab = Resources.Load<GameObject>("NodeConnection").GetComponent<NodeConnection>();
        }

        private void Awake()
        {
            _pointerTarget.OnClick += MovePlayer;
        }

        private void MovePlayer(PointerType button, Vector3 contactPoint)
        {
            print("Move");
            if (button != PointerType.Left)
                return;
            
            if (GetAdjacentPlayerNode() != null)
            {
                _player.MoveToNode(this);
            }
        }

        private void OnValidate()
        {
            CreateConnection();
        }

        private Node GetAdjacentPlayerNode()
        {
            foreach (NodeConnection connection in _connections)
            {
                Node otherNode = connection.GetOtherNode(this);
                if (_player.CurrentNode.Equals(otherNode))
                    return otherNode;
            }
            return null;
        }

        private void CreateConnection()
        {
            if (!_createConnection)
                return;

            _createConnection = false;
            GameObject[] chosenObjects = Selection.gameObjects;
            if (chosenObjects.Length != 2)
                throw new Exception("Select 2 Nodes");
            if (!gameObject.Equals(chosenObjects[0]))
                return;
            
            Node[] nodes = chosenObjects.Select(obj => obj.GetComponent<Node>()).Where(node => node != null).ToArray();
            
            if (nodes.Length != 2)
                throw new Exception("Select 2 Nodes");
            
            foreach (Node node in nodes)
            {
                node._createConnection = false;
            }

            NodeConnection connection = PrefabUtility.InstantiatePrefab(_connectionPrefab.gameObject).GetComponent<NodeConnection>();
            connection.transform.position = Vector3.Lerp(((Component) nodes[0]).transform.position, ((Component) nodes[1]).transform.position, 0.5f);
            connection.Init(nodes);
            print($"Connection between {nodes[0].gameObject} and {nodes[1].gameObject} created!");
        }
    }
}