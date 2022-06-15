using Model.Travel.Dice;
using UnityEngine;

namespace Model.Travel.Map.MoveRequirements
{
    public abstract class MoveRequirement : MonoBehaviour
    {
        [SerializeField] private bool _rotate;

        public bool Rotate => _rotate;
        
        public abstract bool MeetsRequirements(TravelDie die);
    }
}