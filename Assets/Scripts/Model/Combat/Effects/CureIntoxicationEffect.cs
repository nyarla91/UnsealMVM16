using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class CureIntoxicationEffect : Effect
    {
        private readonly Player _player;
        private readonly int _value;

        public CureIntoxicationEffect(float dealyAfter, Player player, int value) : base(dealyAfter)
        {
            _player = player;
            _value = value;
        }

        public override void Execute()
        {
            _player?.RemoveIntoxication(_value);
        }
    }
}