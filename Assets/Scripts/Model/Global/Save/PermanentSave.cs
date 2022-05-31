using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Model.Global.Save
{
    public class PermanentSave : Save
    {
        private PermanentSaveData _data;

        public PermanentSaveData Data => _data;

        public void Save() => SaveData(_data, "permanent");

        public void Load()
        {
            if (!TryLoadData(out _data, "permanent"))
            {
                _data = new PermanentSaveData();
            }
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Load();
        }
    }

    [Serializable]
    public class PermanentSaveData : SavedData
    {
        [SerializeField] private List<string> _cardsUnlocked;

        public List<string> CardsUnlocked
        {
            get => _cardsUnlocked;
            set => _cardsUnlocked = value;
        }

        public PermanentSaveData()
        {
            _cardsUnlocked = new []
            {
                "Model.Cards.Spells.Blood.BiteSpell",
                "Model.Cards.Spells.Blood.RoughWoundSpell",
                "Model.Cards.Spells.Sun.IlluminationSpell",
                "Model.Cards.Spells.Nature.SaplingSpell",
                "Model.Cards.Spells.Nature.SpikedVineSpell",
                "Model.Cards.Spells.Moon.EclipseSpell",
                "Model.Cards.Spells.Moon.LunarFlashSpell",
                "Model.Cards.Spells.Moon.MiracleSpell"
            }.ToList();
        }
    }
}