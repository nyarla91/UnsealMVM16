using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Global.Save
{
    public class ManualSave : Save
    {
        private ManualSaveData _data;

        public ManualSaveData Data => _data;
        
        public void Save() => SaveData(_data, "manual");

        public void Load()
        {
            if (!TryLoadData(out _data, "manual"))
            {
                _data = new ManualSaveData();
            }
        }
    }

    [Serializable]
    public class ManualSaveData : SavedData
    {
        [SerializeField] private List<string> _deck;

        public List<string> Deck
        {
            get => _deck;
            set => _deck = value;
        }
    }
}