using Model.Combat.Characters;
using UnityEngine;

namespace Model.Combat.Effects
{
    public class LoseArmorEffect : Effect
    {
        private readonly Character _target;
        private readonly int _armor;

        public LoseArmorEffect(float dealyAfter, Character target, int armor) : base(dealyAfter)
        {
            _target = target;
            _armor = armor;
        }

        public override void Execute()
        {
            _target?.LoseArmor(_armor);
        }
    }
}