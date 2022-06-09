using System;
using Model.Cards;
using Model.Cards.Combat;
using Model.Cards.Spells;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerBoard : CombatCardArea<CardOnBoard>
    {
        protected override int MaxSize => FormDifferenceBuff ? 7 : 6;

        public int PassiveModifier { get; set; } = 1;

        public int Burst
        {
            get => _burst;
            private set
            {
                if (_burst != value)
                    OnBurstChanged?.Invoke(value);
                _burst = value;
            }
        }

        public Action<Spell> OnCardPurged;
        public event Action<int> OnBurstChanged;
        private int _burst = 0;
        public bool FormDifferenceBuff { private get; set; }

        [DontCallFromSpells]
        public void AddGrowth(int burst) => Burst += burst;

        public bool TrySpendGrowth()
        {
            if (Burst <= 0)
                return false;
            Burst--;
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
