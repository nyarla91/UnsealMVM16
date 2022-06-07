using Model.Travel.Dice;
using UnityEngine;

namespace Model.Travel.Map.MoveRequirements
{
    public class SideRequirement : MoveRequirement
    {
        [SerializeField] private TravelDieSide _requiredSide;

        public override bool MeetsRequirements(TravelDie die) => die.CurrentSide == _requiredSide;
    }
}