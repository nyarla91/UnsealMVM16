using Model.Global.Save;
using Presenter.Combat;
using UnityEngine;

namespace Model.Travel.Map.Rewards
{
    [CreateAssetMenu(fileName = "Form", menuName = "Combat Rewards/Form", order = 0)]
    public class FormReward : CombatReward
    {
        public override void ClaimReward(PermanentSave permanentSave, ManualSave manualSave)
        {
            permanentSave.Data.FormsUnlcoked.Add(ExamplePrefab.name);
        }

        public override void ShowExample(RectTransform parent, CombatEndPresenter presenter)
        {
            Transform form = InstantiateExample<Transform>(parent);
            form.localPosition = new Vector3(0, 0, -20);
            form.localRotation = Quaternion.Euler(-75, -90, 270);
            form.localScale = Vector3.one * 200; 
        }
    }
}