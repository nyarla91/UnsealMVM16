using System.Threading.Tasks;
using Model.Combat.Effects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Combat.Characters
{
    public class Enemy : Character
    {
        [SerializeField] private int _minAttack;
        [SerializeField] private int _maxAttack;
        [SerializeField] private int _minArmor;
        [SerializeField] private int _maxArmor;

        private int RollAttack => Random.Range(_minAttack, _maxAttack + 1);
        private int RollArmor => Random.Range(_minArmor, _maxArmor + 1);
        
        private void Awake()
        {
            GameBoard.Turn.OnEnemyTurnStart += OnEnemyTurnStart;
            GameBoard.AddEnemy(this);
        }

        private async void OnEnemyTurnStart()
        {
            EffectQueue queue = GameBoard.EffectQueue;
            queue.Delay(0.3f);
            queue.InsertEffect(new DealDamageEffect(0.4f, GameBoard.Player, RollAttack, false), 0);
            queue.InsertEffect(new AddArmorEffect(0.2f, this, RollArmor, false), 0);
            await Task.Delay(25);
            queue.InsertEffect(new TriggerPereodicDamageEffect(0.2f, this), 0);
            await Task.Delay(25);
            queue.InsertEffect(new ClearArmorEffect( 0.1f, this), 0);
            
        }

        private void OnValidate()
        {
            if (_maxAttack < _minAttack)
                _maxAttack = _minAttack;
            if (_maxArmor < _minArmor)
                _maxArmor = _minArmor;
        }

        private void OnDestroy()
        {
            GameBoard.Turn.OnEnemyTurnStart -= OnEnemyTurnStart;
            GameBoard.RemoveEnemy(this);
        }
    }
}