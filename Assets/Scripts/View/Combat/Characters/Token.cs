using System;
using DG.Tweening;
using Essentials;
using TMPro;
using UnityEngine;

namespace View.Combat.Characters
{
    public class Token : DescendingObject
    {
        [SerializeField] private bool _startOnGround;
        [SerializeField] private TextMeshPro[] _textMeshes;
        [SerializeField] private Transform _mesh;
        [SerializeField] private Transform _nextStandart;
        [SerializeField] private Transform _previousStandart;
        
        private int _value;

        protected override Transform MovedObject => _mesh;

        public Vector3 TargetPosition { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                foreach (TextMeshPro textMesh in _textMeshes)
                {
                    textMesh.text = value.ToString();
                }
                _value = value;
            }
        }

        public void Flip()
        {
            _mesh.DOComplete();
            _mesh.DORotateQuaternion(_nextStandart.rotation, 0.4f);
            _mesh.DOJump(transform.position, 5, 1, 0.4f);
            MiscEssentials.Swap(ref _previousStandart, ref _nextStandart);
        }

        private void Awake()
        {
            TargetPosition = transform.position;
        }

        private void Start()
        {
            if (!_startOnGround)
                AscendImmediately();
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 10;
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.fixedDeltaTime * MovementSpeed);
        }
    }
}