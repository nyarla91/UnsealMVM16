using Model.Travel.Dice;
using UnityEngine;

namespace Model.Travel.Map.MoveRequirements
{
    public abstract class MoveRequirement : MonoBehaviour
    {
        public abstract bool MeetsRequirements(TravelDie die);
    }
}