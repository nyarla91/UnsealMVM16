using Model.Global.Save;
using Model.Travel.Dice;
using UnityEngine;
using Zenject;

namespace Model.Travel.Map.MoveRequirements
{
    public class FormReuqirement : MoveRequirement
    {
        [SerializeField] private GameObject _requiredForm;
        
        [Inject] private PermanentSave _permanentSave;

        public override bool MeetsRequirements(TravelDie die) => _permanentSave.Data.FormsUnlcoked.Contains(_requiredForm.name);
    }
}