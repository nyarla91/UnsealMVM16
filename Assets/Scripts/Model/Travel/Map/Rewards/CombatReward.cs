using Model.Global.Save;
using Presenter.Combat;
using UnityEngine;

namespace Model.Travel.Map.Rewards
{
    public abstract class CombatReward : ScriptableObject
    {
        [SerializeField] private GameObject _examplePrefab;
        
        protected GameObject ExamplePrefab => _examplePrefab;
        
        public abstract void ClaimReward(PermanentSave save);

        public abstract void ShowExample(RectTransform parent, CombatEndPresenter presenter);

        protected T InstantiateExample<T>(RectTransform parent)
        {
            return Instantiate(_examplePrefab, parent).GetComponent<T>();
        }
    }
}