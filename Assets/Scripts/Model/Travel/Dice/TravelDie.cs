using System;
using Essentials;
using Essentials.Pointers;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;
using Random = UnityEngine.Random;

namespace Model.Travel.Dice
{
    public class TravelDie : Transformer
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private TravelDieSide[] _sides = new TravelDieSide[6];

        private TravelDicePool _dicePool;

        public event Action<int, TravelDieSide> OnRoll;
        
        public TravelDieSide[] Sides => _sides;

        public TravelDieSide CurrentSide { get; private set; }
        public bool Exhausted { get; private set; }
        public Vector3 TargetLocalPosition { get; set; }

        public void Init(TravelDicePool dicePool)
        {
            _dicePool = dicePool;
            _pointerTarget.OnDown += Select;
            _pointerTarget.OnDragEnd += Deselect;
        }
        
        public void Exhaust()
        {
            if (Exhausted)
                return;
            Exhausted = true;
            _dicePool?.RearrangeDice();
        }

        public void Ready()
        {
            if (!Exhausted)
                return;
            Exhausted = false;
            _dicePool?.RearrangeDice();
        }

        public void Roll()
        {
            int sideIndex = Random.Range(0, 6);
            CurrentSide = _sides[sideIndex];
            OnRoll?.Invoke(sideIndex, CurrentSide);
        }

        private void Select(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;
            _dicePool?.SelectDie(this);
        }

        private void Deselect(PointerType button)
        {
            if (button != PointerType.Left)
                return;
            _dicePool.DeselectDie();
        }

        private void Start()
        {
            Roll();
            if (TargetLocalPosition.Equals(Vector3.zero))
                TargetLocalPosition = transform.localPosition;
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 12;
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetLocalPosition, Time.fixedDeltaTime * MovementSpeed);
        }
    }

    public enum TravelDieSide
    {
        Walk,
        River,
        Obstacle,
        Corruption,
        Seal
    }
}