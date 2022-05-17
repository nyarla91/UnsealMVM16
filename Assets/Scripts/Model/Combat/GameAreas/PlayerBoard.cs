using Model.Combat.Cards;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public sealed class PlayerBoard : CardArea<CardOnBoard>
    {
        public override int MaxSize => 6;
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
