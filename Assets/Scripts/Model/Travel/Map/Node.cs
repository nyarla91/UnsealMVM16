using System.Collections.Generic;
using Essentials;
using Essentials.Pointers;
using Model.Travel.Dice;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Travel.Map
{
    public class Node : Transformer
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private List<NodeConnection> _connections;

        private PlayerMiniature _player;
        private TravelDicePool _dicePool;

        public List<NodeConnection> Connections => _connections;

        [Inject]
        private void Construct(PlayerMiniature player, TravelDicePool dicePool)
        {
            _player = player;
            _dicePool = dicePool;
        }


        public void AddConnection(NodeConnection connection)
        {
            _connections.Add(connection);
        }

        public void RemoveConnection(NodeConnection connection)
        {
            _connections.Remove(connection);
        }

        private void UseDieOnThis(PointerType button, Vector3 contactPoint)
        {
            print(GetAdjacentPlayerNode(out NodeConnection connectdion));
            if (button != PointerType.Left || !_dicePool.IsDieSelected(out TravelDie selectedDie) || GetAdjacentPlayerNode(out NodeConnection connection)== null)
                return;

            if (!connection.CompareSide(selectedDie.CurrentSide))
                return;
            
            _player.MoveToNode(this);
            selectedDie.Exhaust();
            OnPlayerEnter();
        }

        protected virtual void OnPlayerEnter() {}

        private Node GetAdjacentPlayerNode() => GetAdjacentPlayerNode(out NodeConnection nodeConnection);

        private Node GetAdjacentPlayerNode(out NodeConnection nodeConnection)
        {
            foreach (NodeConnection connection in _connections)
            {
                Node otherNode = connection.GetOtherEnd(this);
                if (_player.CurrentNode.Equals(otherNode))
                {
                    nodeConnection = connection;
                    return otherNode;
                }
            }
            nodeConnection = null;
            return null;
        }

        private void Awake()
        {
            _pointerTarget.OnUp += UseDieOnThis;
        }
    }
}