using System;
using System.Collections.Generic;
using DG.Tweening;
using Essentials;
using UnityEngine;

namespace View.Combat.Characters
{
    public class Counter : DescendingObject
    {
        [SerializeField] private Transform _tensWheel;
        [SerializeField] private Transform _unitsWheel;

        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                value %= 100;
                SetWheel(_tensWheel, value / 10);
                SetWheel(_unitsWheel, value % 10);
            }
        }

        private void Awake()
        {
            AscendImmediately();
        }

        private void SetWheel(Transform wheel, int value)
        {
            wheel.DOKill();
            wheel.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, -value * 36), 0.6f);
        }
    }
}