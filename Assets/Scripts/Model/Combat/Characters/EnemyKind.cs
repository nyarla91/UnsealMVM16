using UnityEngine;

namespace Model.Combat.Characters
{
    public class EnemyKind : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _minAttack;
        [SerializeField] private int _maxAttack;
        [SerializeField] private int _minArmor;
        [SerializeField] private int _maxArmor;

        public int Health => _health;
        public int MinArmor => _minArmor;
        public int MaxArmor => _maxArmor;
        public int MinAttack => _minAttack;
        public int MaxAttack => _maxAttack;
        
        private void OnValidate()
        {
            if (_maxAttack < _minAttack)
                _maxAttack = _minAttack;
            if (_maxArmor < _minArmor)
                _maxArmor = _minArmor;
        }
    }
}