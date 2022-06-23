using System;
using Model.Global.Save;
using Model.Travel.Dice;
using Model.Travel.Map.MoveRequirements;
using Presenter.Travel;
using Presenter.Travel.Camera;
using UnityEngine;
using Zenject;

namespace Model.Travel.Map
{
    public class NodeConnection : MonoBehaviour
    {
        [SerializeField] private TravelObject _travelObject;
        [SerializeField] private bool _moveToEnds;
        [SerializeField] private bool _validateEndsHaveThis;
        [SerializeField] private Node[] _ends;

        private MoveRequirement _requirement;
        
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

        private PermanentSave _permanentSave;
        private TravelCamera _travelCamera;


        public void PassDependencies(PermanentSave permanentSave, TravelCamera travelCamera) =>
            _travelObject.Construct(permanentSave, travelCamera);

        public Node GetOtherEnd(Node otherThanThis) => _ends[0].Equals(otherThanThis) ? _ends[1] : _ends[0];

        public bool CheckPatency(TravelDie die) => _requirement == null || _requirement.MeetsRequirements(die);

        private void Start()
        {
            Collider[] overlap = Physics.OverlapSphere(transform.position, 0.01f);
            if (overlap.Length == 0 || !overlap[0].TryGetComponent(out MoveRequirement requirement))
                return;

            _requirement = requirement;
            if (requirement.Rotate)
                requirement.transform.rotation = Quaternion.LookRotation(_ends[0].transform.position - _ends[1].transform.position, Vector3.up);
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