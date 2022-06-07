using Model.Global.Save;
using Model.Travel.Dice;
using UnityEngine;
using Zenject;

namespace Model.Travel.Map.MoveRequirements
{
    public class KeyRequirement : MoveRequirement
    {
        [SerializeField] private int _keys;

        
        [Inject] private PermanentSave _permanentSave;
        
        public int Keys => _keys;
        
        public override bool MeetsRequirements(TravelDie die) => _permanentSave.Data.Keys >= _keys;
    }
}