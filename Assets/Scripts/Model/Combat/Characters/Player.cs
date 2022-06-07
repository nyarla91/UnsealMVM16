using System;
using Model.Combat.Effects;
using Model.Global;
using UnityEngine;
using Zenject;

namespace Model.Combat.Characters
{
    public class Player : Character
    {
        private const int IntoxicationCap = 4;
        
        [Inject] private GlobalTravelState _globalTravelState;
        
        private int _intoxication;

        public event Action<int> OnIntoxicationAdded;
        public event Action<int> OnIntoxicationCured;

        [DontCallFromSpells]
        public void RemoveIntoxication(int value)
        {
            if (value <= 0)
                return;
            value = Mathf.Min(value, _intoxication);
            OnIntoxicationCured?.Invoke(value);
            _intoxication -= value;
        }
        
        [DontCallFromSpells]
        public void AddIntoxication(int value)
        {
            if (value <= 0)
                return;

            OnIntoxicationAdded?.Invoke(Mathf.Min(value, IntoxicationCap - _intoxication));
            _intoxication += value;
            while (_intoxication > IntoxicationCap)
            {
                LoseHealth(4);
                _intoxication--;
            }
        }
        
        private void Awake()
        {
            GameBoard.Turn.OnPlayerTurnStart += OnPlayerTurnStart;
            MaxHealth = _globalTravelState.PlayerHealth;
            Health = MaxHealth;
        }

        private async void OnPlayerTurnStart()
        {
            EffectQueue queue = GameBoard.EffectQueue;
            queue.Delay(0.5f);
            queue.AddEffect(new ClearArmorEffect( 0.4f, this));
            queue.AddEffect(new TriggerPereodicDamageEffect(0.2f, this));
        }

        private void OnDestroy()
        {
            GameBoard.Turn.OnPlayerTurnStart -= OnPlayerTurnStart;
        }
    }
}