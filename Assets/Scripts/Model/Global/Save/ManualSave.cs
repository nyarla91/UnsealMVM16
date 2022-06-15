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
                "Model.Cards.Spells.Action.HealingPotionSpell",
                "Model.Cards.Spells.Action.FieryBreathPotionSpell",
                "Model.Cards.Spells.Action.DefensivePotionSpell",
                "Model.Cards.Spells.Action.MetabolicBoostSpell",
                "Model.Cards.Spells.Action.AntidoteSpell",
                "Model.Cards.Spells.Blood.BloodVialSpell",
                "Model.Cards.Spells.Blood.AcidThrowSpell",
                "Model.Cards.Spells.Moon.PotionOfMadessSpell",
                "Model.Cards.Spells.Moon.MoonlightPotionSpell",
                "Model.Cards.Spells.Nature.CausticVaporsSpell",
                "Model.Cards.Spells.Nature.MedicinalHerbsSpell",
                "Model.Cards.Spells.Sun.HolyWaterSpell",
                "Model.Cards.Spells.Sun.BlindingPotionSpell",
                "Model.Cards.Spells.Action.AlignmentSpell",
                "Model.Cards.Spells.Action.EncryptingSpell",
                "Model.Cards.Spells.Action.EnergyReleaseSpell",
                "Model.Cards.Spells.Action.SealOfProtectionSpell",
                "Model.Cards.Spells.Action.TheGreatSealSpell",
                "Model.Cards.Spells.Action.UnsealSpell",
                "Model.Cards.Spells.Blood.BloodRainSpell",
                "Model.Cards.Spells.Blood.SealOfTheBloodSpell",
                "Model.Cards.Spells.Blood.PayWithBloodSpell",
                "Model.Cards.Spells.Moon.SealOfTheMoonSpell",
                "Model.Cards.Spells.Moon.DashSpell",
                "Model.Cards.Spells.Moon.AwakeningSpell",
                "Model.Cards.Spells.Nature.SealOfTheNatureSpell",
                "Model.Cards.Spells.Nature.SeedingSpell",
                "Model.Cards.Spells.Nature.LakePortalSpell",
                "Model.Cards.Spells.Sun.SealOfTheSunSpell",
                "Model.Cards.Spells.Sun.BlindingRaysSpell",
                "Model.Cards.Spells.Sun.ApocalypseSpell",
            }.ToList();
        }
    }
}