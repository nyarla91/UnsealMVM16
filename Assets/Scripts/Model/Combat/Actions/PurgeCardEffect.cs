using Model.Combat.Cards;
using UnityEngine;

namespace Model.Combat.Actions
{
    public class PurgeCardEffect : Effect
    {
        private readonly CardOnBoard _cardToPurge;

        public PurgeCardEffect(float dealyAfter, CardOnBoard cardToPurge) : base(dealyAfter)
        {
            _cardToPurge = cardToPurge;
        }


        public override void Execute()
        {
            _cardToPurge.Purge();
        }
    }
}