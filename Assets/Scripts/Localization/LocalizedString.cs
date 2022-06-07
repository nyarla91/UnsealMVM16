using System;
using Localization;
using UnityEngine;

namespace Model.Localization
{
    [Serializable]
    public class LocalizedString
    {
        [SerializeField] [TextArea(1, 15)] private string _eng;
        [SerializeField] [TextArea(1, 15)] private string _rus;

        public string Eng => _eng;
        public string Rus => _rus;
        
        public string Localized => Language.language == 0 ? Eng : Rus;

        public LocalizedString(string eng, string rus)
        {
            _eng = eng;
            _rus = rus;
        }

        public bool Contains(string value) => Eng.Contains(value) || Rus.Contains(value);

        public static implicit operator string(LocalizedString localizedString) => localizedString.Localized;
        public override string ToString() => Localized;
    }
}