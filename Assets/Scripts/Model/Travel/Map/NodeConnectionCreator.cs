using Essentials;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Travel.Map
{
    public class NodeConnectionCreator : ComponentInstantiator
    {
        private const float NodeDistance = 5;
            
        private static Vector3[] _allDirections;
        private static Vector3[] AllDirections
        {
            get
            {
                if (_allDirections == null)
                {
                    _allDirections = new Vector3[6];
                    for (var i = 0; i < _allDirections.Length; i++)
                    {
                        _allDirections[i] = ((float) i * 60).DegreesToVector2().XYtoXZ() * NodeDistance;
                    }
                }
                return _allDirections;
            }
        }

        [SerializeField] private Node _node;
        [SerializeField] private float _creationOrder;
        [SerializeField] private NodeConnection _connectionPrefab;

        [ContextMenu("Randomize Order")]
        private void RandomizeOrder()
        {
            _creationOrder = Random.Range(0, (float) 10000);
        }

        [ContextMenu("Create Connections")]
        private void Start()
        {
            foreach (var direction in AllDirections)
            {
                TryCreateConnection(direction);
            }
        }

        private void TryCreateConnection(Vector3 direction)
        {
            Collider[] overlap = Physics.OverlapSphere(transform.position + direction, 0.1f);
            if (overlap.Length == 0)
                return;

            Node other = overlap[0].GetComponent<Node>();
            if (other.GetComponent<NodeConnectionCreator>()._creationOrder > _creationOrder)
                return;

            Node[] nodes = { _node, other };

            Vector3 position = Vector3.Lerp(nodes[0].transform.position, nodes[1].transform.position, 0.5f);
            InstantiateForComponent(out NodeConnection connection, _connectionPrefab, position);
            connection.Init(nodes);
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var direction in AllDirections)
            {
                Vector3 destination = transform.position + direction;
                Gizmos.DrawLine(transform.position, destination);
                Gizmos.DrawSphere(destination, 0.4f);
            }
        }
    }
}