using Model.Global.Save;
using Presenter.Combat;
using UnityEngine;

namespace Model.Travel.Map.Rewards
{
    [CreateAssetMenu(fileName = "Key", menuName = "Combat Rewards/Key", order = 0)]
    public class KeyReward : CombatReward
    {
        public override void ClaimReward(PermanentSave permanentSave, ManualSave manualSave)
        {
            permanentSave.Data.AddKey();
            permanentSave.Save();
        }

        public override void ShowExample(RectTransform parent, CombatEndPresenter presenter)
        {
            Transform key = InstantiateExample<Transform>(parent);
            key.localPosition = new Vector3(0, 0 -20);
            key.localRotation = Quaternion.Euler(-90, 0, 0);
            key.localScale = Vector3.one * 120;
        }
    }
}