using System;
using Model.Cards;
using Model.Cards.Spells;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerBoard : CardArea<CardOnBoard>
    {
        public override int MaxSize => 5;

        public int PassiveModifier { get; set; } = 1;

        public int Growth
        {
            get => _growth;
            private set
            {
                if (_growth != value)
                    OnGrowthChanged?.Invoke(value);
                _growth = value;
            }
        }

        public Action<Spell> OnCardPurged;
        public event Action<int> OnGrowthChanged;
        private int _growth = 0;

        [DontCallFromSpells]
        public void AddGrowth(int growth) => Growth += growth;

        public bool TrySpendGrowth()
        {
            if (Growth <= 0)
                return false;
            Growth--;
            return true;
        }
        
        protected override void RearrangeCards()
        {
            if (Size == 0)
                return;

            float leftMostIndex = (1 - Size) * 0.5f;
            const float unitsPerIndex = 2.2f;
            for (int i = 0; i < Size; i++)
            {
                Vector3 localPosition = new Vector3((leftMostIndex + i) * unitsPerIndex, 0, 0);
                Cards[i].TargetPosition = CardStandart.TransformPoint(localPosition);
                Cards[i].TargetRotation = CardStandart.rotation;
            }
        }
    }
}
