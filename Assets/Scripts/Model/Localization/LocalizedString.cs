using System;
using UnityEngine;

namespace Model.Localization
{
    [Serializable]
    public class LocalizedString
    {
        [SerializeField] [TextArea(1, 15)] private string _eng;
        [SerializeField] [TextArea(1, 15)] private string _rus;

        public string Localized => _eng;
    }
}