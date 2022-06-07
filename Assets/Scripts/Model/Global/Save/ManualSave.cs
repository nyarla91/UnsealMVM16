using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Load();
        }
    }

    [Serializable]
    public class ManualSaveData : SavedData
    {
        [SerializeField] private List<string> _deck;
        [SerializeField] private Vector3 _nodePosition = Vector3.down;

        public List<string> Deck
        {
            get => _deck;
            set => _deck = value;
        }

        public Vector3 NodePosition
        {
            get => _nodePosition;
            set => _nodePosition = value;
        }

        public ManualSaveData()
        {
            _deck = new []
            {
                "Model.Cards.Spells.Sun.IlluminationSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Action.DodgeSpell",
                "Model.Cards.Spells.Blood.BiteSpell",
                "Model.Cards.Spells.Blood.RoughWoundSpell",
                "Model.Cards.Spells.Nature.SaplingSpell",
                "Model.Cards.Spells.Moon.MiracleSpell",
            }.ToList();
        }
    }
}