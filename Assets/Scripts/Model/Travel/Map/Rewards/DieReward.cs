using Model.Global.Save;
using Presenter.Combat;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Model.Travel.Map.Rewards
{
    [CreateAssetMenu(fileName = "Die", menuName = "Combat Rewards/Die", order = 0)]
    public class DieReward : CombatReward
    {
        public override void ClaimReward(PermanentSave save)
        {
            save.Data.Dice.Add(ExamplePrefab.name);
            save.Save();
        }

        public override void ShowExample(RectTransform parent, CombatEndPresenter presenter)
        {
            Transform die = InstantiateExample<Transform>(parent);
            die.localPosition = new Vector3(0, -10, -80);
            die.localRotation = Quaternion.Euler(45, 0, 45);
            die.localScale = Vector3.one * 200;
        }
    }
}