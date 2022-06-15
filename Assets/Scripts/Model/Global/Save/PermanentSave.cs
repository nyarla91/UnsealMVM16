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

        private void Load()
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
        [SerializeField] private List<string> _formsUnlcoked;
        [SerializeField] private List<string> _shrinesUnlocked;
        [SerializeField] private List<string> _map;
        [SerializeField] private List<string> _dice;
        [SerializeField] private List<string> _combatsCleared;
        [SerializeField] private List<string> _tutorialsWatched;
        [SerializeField] private int _keys;

        public List<string> CardsUnlocked
        {
            get => _cardsUnlocked;
            private set => _cardsUnlocked = value;
        }

        public List<string> FormsUnlcoked
        {
            get => _formsUnlcoked;
            private set => _formsUnlcoked = value;
        }

        public List<string> Dice
        {
            get => _dice;
            private set => _dice = value;
        }

        public List<string> CombatsCleared
        {
            get => _combatsCleared;
            private set => _combatsCleared = value;
        }

        public int Keys
        {
            get => _keys;
            private set => _keys = value;
        }

        public List<string> ShrinesUnlocked
        {
            get => _shrinesUnlocked;
            set => _shrinesUnlocked = value;
        }

        public List<string> Map
        {
            get => _map;
            set => _map = value;
        }

        public List<string> TutorialsWatched
        {
            get => _tutorialsWatched;
            set => _tutorialsWatched = value;
        }

        public void AddKey() => Keys++;

        public PermanentSaveData()
        {
            _combatsCleared = new List<string>();
            _shrinesUnlocked = new List<string>();
            _tutorialsWatched = new List<string>();
            _map = new List<string>();
            
            _cardsUnlocked = new []
            {
                "Model.Cards.Spells.Action.HitSpell",
                "Model.Cards.Spells.Action.DodgeSpell"
            }.ToList();

            _formsUnlcoked = new[]
            {
                "RegularForm"
            }.ToList();
            
            _dice = new []
            {
                "StartingDie",
                "StartingDie",
                "StartingDie"
            }.ToList();
        }
    }
}