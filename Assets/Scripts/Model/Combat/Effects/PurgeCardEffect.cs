using Model.Cards;
using Model.Cards.Combat;

namespace Model.Combat.Effects
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
            if (_cardToPurge != null)
                _cardToPurge.Purge();
        }
    }
}