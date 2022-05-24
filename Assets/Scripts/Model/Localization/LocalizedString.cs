using System;
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
        
        public string Localized => _eng;

        public LocalizedString(string eng, string rus)
        {
            _eng = eng;
            _rus = rus;
        }
    }
}