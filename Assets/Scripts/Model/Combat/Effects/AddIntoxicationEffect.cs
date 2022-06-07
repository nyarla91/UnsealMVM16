using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class AddIntoxicationEffect : Effect
    {
        private readonly Player _player;
        private readonly int _value;

        public AddIntoxicationEffect(float dealyAfter, Player player, int value) : base(dealyAfter)
        {
            _player = player;
            _value = value;
        }

        public override void Execute()
        {
            _player?.AddIntoxication(_value);
        }
    }
}