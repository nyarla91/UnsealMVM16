using Model.Localization;
using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizedTextMesh : MonoBehaviour
    {
        [SerializeField] private LocalizedString _string;
        
        private TMP_Text _text;

        public TMP_Text Text => _text ??= GetComponent<TMP_Text>();

        public LocalizedString String
        {
            get => _string;
            set
            {
                _string = value;
                Text.text = value.Localized;
            }
        }

        private void Awake()
        {
            String = _string;
        }
    }
}