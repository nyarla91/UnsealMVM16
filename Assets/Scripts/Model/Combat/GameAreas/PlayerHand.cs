using System;
using Model.Cards;
using Model.Cards.Combat;
using Model.Cards.Spells;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerHand : CombatCardArea<CardInHand>
    {
        protected override int MaxSize => 10;

        public Action<Spell> OnSpellPlayed;
        public SpellType ForbiddenType { get; set; } = SpellType.None;
        
        protected override void RearrangeCards()
        {
            if (Size == 0)
                return;

            float leftMostIndex = (1 - Size) * 0.5f;
            const float UnitsPerIndex = 1.5f;
            for (int i = 0; i < Size; i++)
            {
                Vector3 localPosition = new Vector3((leftMostIndex + i) * UnitsPerIndex, i * 0.02f, 0);
                Cards[i].TargetPosition = CardStandart.TransformPoint(localPosition);
                Cards[i].TargetRotation = CardStandart.rotation;
            }
        }
    }
}