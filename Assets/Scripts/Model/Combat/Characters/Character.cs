using System;
using Model.Combat.Actions;
using UnityEngine;

namespace Model.Combat.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private int _health;

        public int MaxHealth => _maxHealth;

        public int Health
        {
            get => _health;
            set
            {
                if (_health == value)
                    return;
                
                OnHealthChanged?.Invoke(value);
                _health = value;
            }
        }

        public event Action<int> OnHealthChanged;

        [DontCallFromSpells]
        public void DealDamage(int damage)
        {
            Health -= damage;
        }

        private void Start()
        {
            Health = MaxHealth;
        }
    }
}