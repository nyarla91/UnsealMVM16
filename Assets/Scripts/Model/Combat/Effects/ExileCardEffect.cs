using Model.Cards;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class ExileCardEffect : Effect
    {
        private readonly Card _target;

        public ExileCardEffect(float dealyAfter, Card target) : base(dealyAfter)
        {
            _target = target;
        }

        public override void Execute()
        {
            _target?.Exile();
        }
    }
}