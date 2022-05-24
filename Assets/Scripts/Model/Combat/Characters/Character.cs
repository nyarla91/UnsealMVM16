using System;
using System.Collections.Generic;
using System.Linq;
using Model.Combat.Effects;
using Model.Combat.GameAreas;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Model.Combat.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private int _health;
        private int _armor;
        protected List<int> _pereodicDamage = new List<int>();
        
        [field: Inject]
        protected GameBoard GameBoard { get; set; }

        public int MaxHealth => _maxHealth;

        public int Health
        {
            get => _health;
            set
            {
                value = Mathf.Max(value, 0);
                if (_health == value)
                    return;
                
                if (value < _health)
                    OnLoseHealth?.Invoke(_health - value);
                OnHealthChanged?.Invoke(value);
                _health = value;
            }
        }

        public int Armor
        {
            get => _armor;
            private set
            {
                value = Mathf.Max(value, 0);
                if (_armor == value)
                    return;

                OnArmorChanged?.Invoke(value);
                _armor = value;
            }
        }

        public List<int> PereodicDamage => _pereodicDamage.Clone(new ListCloner(), false);

        public event Action<int> OnHealthChanged;
        public event Action<int> OnTakeDamage;
        public event Action<int> OnLoseHealth;
        public event Action<int> OnRestoreHealth;
        public event Action<int> OnArmorChanged;
        public event Action<int> OnArmorAdded;
        public event Action<int> OnBleedAdded;
        public event Action<int, int> OnBleedValueChanged;
        public event Action<int> OnBleedRemoved;

        [DontCallFromSpells]
        public void DealDamage(int damage)
        {
            if (damage <= 0)
                return;
            
            OnTakeDamage?.Invoke(damage);
            if (damage > Armor)
                Health -= damage - Armor;
            Armor -= damage;
        }

        [DontCallFromSpells]
        public void DealPereodicDamage(int damage)
        {
            if (damage <= 0)
                return;
            
            OnBleedAdded?.Invoke(damage);
            _pereodicDamage.Add(damage);
        }

        [DontCallFromSpells]
        public void RestoreHealth(int health)
        {
            if (health <= 0)
                return;
            
            OnRestoreHealth?.Invoke(health);
            Health += health;
        }

        [DontCallFromSpells]
        public void TriggerPereodicDamage()
        {
            for (int i = _pereodicDamage.Count - 1; i >= 0; i--)
            {
                DealDamage(1);
                _pereodicDamage[i]--;
                if (_pereodicDamage[i] > 0)
                    OnBleedValueChanged?.Invoke(i, _pereodicDamage[i]);
                else
                {
                    OnBleedRemoved?.Invoke(i);
                }
            }
            _pereodicDamage = _pereodicDamage.Where(t => t > 0).ToList();
        }

        [DontCallFromSpells]
        public void AddArmor(int armor)
        {
            if (armor <= 0)
                return;
            
            OnArmorAdded?.Invoke(armor);
            Armor += armor;
        }

        [DontCallFromSpells]
        public void ClearArmor() => Armor = 0;

        private void Start()
        {
            Health = MaxHealth;
            Armor = 0;
            OnArmorChanged?.Invoke(Armor);
        }
    }
}