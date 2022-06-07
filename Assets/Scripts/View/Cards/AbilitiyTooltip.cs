using Essentials;
using Model.Localization;
using TMPro;
using UnityEngine;

namespace View.Cards
{
    public class AbilitiyTooltip : UIDialog
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private SerializedDictionary<string, LocalizedString> _abilities;

        public string DescriptionToExlanation(string description)
        {
            description = description.ToLower();
            string totalText = "";
            foreach (var abilityPair in _abilities.Dictionary)
            {
                if (description.Contains(abilityPair.Key))
                {
                    totalText += abilityPair.Value.Localized;
                    totalText += "\n \n";
                }
            }
            return totalText;
        }
        
        public void Show(string text)
        {
            FadeIn();
            _text.text = CardView.FormatDescription(text);
        }

        public void Hide() => FadeOut();
    }
}