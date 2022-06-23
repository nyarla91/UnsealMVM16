using System;
using System.Threading.Tasks;
using Model.Cards;
using Model.Global.Save;
using Presenter.Cards;
using Presenter.Combat;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using File = System.IO.File;

namespace Model.Travel.Map.Rewards
{
    [CreateAssetMenu(fileName = "Card", menuName = "Combat Rewards/Card", order = 0)]
    public class CardReward : CombatReward
    {
        [SerializeField] private string _spellName;

        private Type _spellType;
        
        public override void ClaimReward(PermanentSave permanentSave, ManualSave manualSave)
        {
            if (permanentSave.Data.CardsUnlocked.Count < 12)
            {
                manualSave.Data.Deck.Add(_spellName);
                manualSave.Save();
            }
            permanentSave.Data.CardsUnlocked.Add(_spellName);
            permanentSave.Save();
        }

        public override void ShowExample(RectTransform parent, CombatEndPresenter presenter)
        {
            Transform example = InstantiateExample<Transform>(parent);
            example.localPosition = new Vector3(0, 0, -5);
            example.localScale = 125 * Vector3.one;
            example.localRotation = Quaternion.Euler(270, 0, 0);
            example.gameObject.GetComponent<CardPresenter>().Tooltip = presenter.AbilitiyTooltip;
            _spellType = Type.GetType(_spellName);
            example.gameObject.AddComponent(_spellType);
            example.gameObject.AddComponent<CardInUI>();
        }

        /*private void OnValidate()
        {
            _spellType = _spell.GetClass();
            if (_spellType.IsSubclassOf(typeof(Spell)))
            {
                _spellName = _spellType.Namespace + "." + _spellType.Name;
            }
            else
            {
                Debug.LogError($"{_spellType.Name} is not derived from Spell");
                _spellName = String.Empty;
                _spellType = null;
            }
        }*/
    }
}