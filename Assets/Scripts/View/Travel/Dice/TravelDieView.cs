using System;
using DG.Tweening;
using Essentials;
using Model.Travel.Dice;
using UnityEngine;

namespace View.Travel.Dice
{
    public class TravelDieView : Transformer
    {
        [SerializeField] private MeshRenderer[] _sideMeshes;
        [SerializeField] private Transform[] _standarts = new Transform[6];

        public float TargetScale { get; set; } = 1;
        
        public void InitSide(int index, TravelDieSide side)
        {
            _sideMeshes[index].material = Resources.Load<Material>($"DiceSides/{side.ToString()}");
        }

        public void RollToSide(int index)
        {
            transform.DOComplete();
            transform.DOLocalRotateQuaternion(_standarts[index].localRotation, 0.4f);
            transform.DOLocalJump(transform.localPosition, 2.5f, 1, 0.4f);
        }

        private void FixedUpdate()
        {
            const float ScaleSpeed = 15;
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, TargetScale, Time.fixedDeltaTime * ScaleSpeed);
        }
    }
}