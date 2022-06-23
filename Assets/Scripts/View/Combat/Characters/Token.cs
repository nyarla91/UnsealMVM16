using System;
using System.Collections.Generic;
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
        [SerializeField] private List<AudioClip> _fallSounds;
        [SerializeField] private AudioSource _audioSource;
        
        private int _value;

        protected override Transform MovedObject => _mesh;

        public Vector3 TargetLocalPosition { get; set; }

        protected virtual bool Move => true;

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
            _audioSource.clip = _fallSounds.PickRandomElement();
            _audioSource.PlayDelayed(0.4f);
        }

        public override void Descend()
        {
            base.Descend();
            _audioSource.clip = _fallSounds.PickRandomElement();
            _audioSource.PlayDelayed(0.4f);
        }

        private void Awake()
        {
            TargetLocalPosition = transform.localPosition;
            if (!_startOnGround)
                AscendImmediately();
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 10;
            if (Move)
                transform.localPosition = Vector3.Lerp(transform.localPosition, TargetLocalPosition, Time.fixedDeltaTime * MovementSpeed);
        }
    }
}