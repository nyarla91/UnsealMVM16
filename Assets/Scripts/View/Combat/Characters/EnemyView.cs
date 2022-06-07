using Model.Combat.Characters;
using TMPro;
using UnityEngine;

namespace View.Combat.Characters
{
    public class EnemyView : DescendingObject
    {
        [SerializeField] private TextMeshPro _healthValue;
        [SerializeField] private TextMeshPro _attackValue;
        [SerializeField] private TextMeshPro _armorValue;
        
        public void UpdateValues(EnemyKind kind)
        {
            _healthValue.text = kind.Health.ToString();
            _attackValue.text = $"{kind.MinAttack} - {kind.MaxAttack}";
            _armorValue.text = $"{kind.MaxArmor} - {kind.MaxArmor}";
        }

        private void Start()
        {
            AscendImmediately();
            Descend();
        }
    }
}