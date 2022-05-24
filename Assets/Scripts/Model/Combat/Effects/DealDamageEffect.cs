using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class DealDamageEffect : Effect
    {
        private readonly Character _character;
        private readonly int _damage;
        private readonly bool _growth;

        public DealDamageEffect(float dealyAfter, Character character, int damage, bool growth) : base(dealyAfter)
        {
            _character = character;
            _damage = damage;
            _growth = growth;
        }

        public override void Execute()
        {
            _character.DealDamage((_growth ? 1 : 0) + _damage);
        }
    }
}