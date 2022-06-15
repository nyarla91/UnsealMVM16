using System;
using System.Collections;
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
        [SerializeField] private NodeConnectionCreator _connectionCreator;
        [SerializeField] private NodeKind _kind;
        [SerializeField] private List<NodeConnection> _connections;

        private PlayerMiniature _player;
        private TravelDicePool _dicePool;
        private bool _interactionActive = true;

        public List<NodeConnection> Connections => _connections;

        public NodeConnectionCreator ConnectionCreator => _connectionCreator;

        public bool InteractionActive
        {
            get => _interactionActive;
            set
            {
                if (_interactionActive == value)
                    return;
                
                _interactionActive = value;
                OnSwitchInteractionActive?.Invoke(value);
            }
        }

        public event Action<bool> OnSwitchInteractionActive;
        public event Action OnPlayerEnterHere;

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

        public void OnPlayerStartHere()
        {
            if (InteractionActive)
                _kind?.OnPLayerStartHere();
        }

        private void StartUseDieOnThis(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;
            
            StartCoroutine(UseDieOnThis());
        }

        private IEnumerator UseDieOnThis()
        {
            Node playerNode = GetAdjacentPlayerNode(out NodeConnection connection);
            if (playerNode == null || !_dicePool.IsDieSelected(out TravelDie selectedDie))
                yield break;

            if (!connection.CheckPatency(selectedDie))
                yield break;
            
            playerNode._kind?.OnPLayerLeave();
            _player.MoveToNode(this);
            if (_kind == null || _kind.SpendDie)
                selectedDie.Exhaust();
            yield return new WaitForSeconds(0.5f);
            OnPlayerEnterHere?.Invoke();
            if (InteractionActive)
                _kind?.OnPLayerEnter();
        }

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
            _pointerTarget.OnUp += StartUseDieOnThis;
        }
    }
}