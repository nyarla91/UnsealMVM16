using DG.Tweening;
using Essentials;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using TMPro;
using UnityEngine;

namespace View.Combat.Characters
{
    public class EnemyView : DescendingObject
    {
        [SerializeField] private TextMeshPro _healthValue;
        [SerializeField] private TextMeshPro _attackValue;
        [SerializeField] private TextMeshPro _armorValue;
        [SerializeField] private Transform _miniature;
        
        public void UpdateValues(Enemy kind)
        {
            _healthValue.text = kind.MaxHealth.ToString();
            _attackValue.text = $"{kind.AttackPerTurn}";
            _armorValue.text = $"{kind.ArmorPerTurn}";
        }

        public void Raise()
        {
            _miniature?.DOComplete();
            _miniature?.DOLocalMove(_miniature.localPosition.WithY(2), 0.2f);
        }

        public void Drop()
        {
            _miniature?.DOComplete();
            _miniature?.DOLocalMove(_miniature.localPosition.WithY(0), 0.2f);
        }

        private void Start()
        {
            AscendImmediately();
            Descend();
        }
    }
}