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
        }

        public void MoveToNodeInstantly(Node destination)
        {
            MoveToNode(destination);
            destination.OnPlayerStartHere();
            transform.position = destination.transform.position;
        }

        private void Start()
        {
            _collider.enabled = true;
            if (TryLoadNodeFromPosition(out Node loadedNode, _globalTravelState.CurrentNodePosition))
                MoveToNodeInstantly(loadedNode);
            else if (TryLoadNodeFromPosition(out loadedNode, _manualSave.Data.NodePosition))
                MoveToNodeInstantly(loadedNode);
            else
                MoveToNodeInstantly(_startingNode);
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
            transform.position = Vector3.Lerp(transform.position, _currentNode.transform.position,
                MovementSpeed * Time.fixedDeltaTime);
        }
    }
}