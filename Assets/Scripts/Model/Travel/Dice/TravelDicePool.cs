using System;
using System.Collections.Generic;
using System.Linq;
using Essentials;
using UnityEngine;

namespace Model.Travel.Dice
{
    public class TravelDicePool : ComponentInstantiator
    {
        [SerializeField] private List<TravelDie> _startingDice;

        private readonly List<TravelDie> _dice = new List<TravelDie>();
        private TravelDie _selectedDie;

        private List<TravelDie> ReadyDice => _dice.Where(die => !die.Exhausted).ToList();
        private List<TravelDie> ExhaustedDice => _dice.Where(die => die.Exhausted).ToList();

        public bool IsDieSelected(out TravelDie die)
        {
            die = _selectedDie;
            return die != null;
        }
        
        public void SelectDie(TravelDie die)
        {
            _selectedDie = die;
        }

        public void DeselectDie()
        {
            _selectedDie = null;
        }

        private void Awake()
        {
            foreach (var startingDie in _startingDice)
            {
                InstantiateForComponent(out TravelDie die, startingDie, transform);
                _dice.Add(die);
                die.Init(this);
                die.transform.localPosition = Vector3.zero;
                die.transform.localRotation = Quaternion.identity;
            }
            RearrangeDice();
        }

        public void RearrangeDice()
        {
            foreach (var exhaustedDie in ExhaustedDice)
            {
                exhaustedDie.TargetLocalPosition = new Vector3(0, -0.5f, -3f);
            }   
            
            if (ReadyDice.Count == 0)
                return;

            float leftMostIndex = (1 - ReadyDice.Count) * 0.5f;
            const float unitsPerIndex = 1.2f;
            for (int i = 0; i < ReadyDice.Count; i++)
            {
                Vector3 localPosition = new Vector3((leftMostIndex + i) * unitsPerIndex, 0, 0);
                ReadyDice[i].TargetLocalPosition = localPosition;
            }
        }
    }
}