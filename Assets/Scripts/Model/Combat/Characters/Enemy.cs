using System.Threading.Tasks;
using Model.Combat.Effects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Combat.Characters
{
    public class Enemy : Character
    {
        [SerializeField] private EnemyKind _kind;
        
        public int RollAttack => Random.Range(_kind.MinAttack, _kind.MaxAttack + 1);
        public int RollArmor => Random.Range(_kind.MinArmor, _kind.MaxArmor + 1);
        
        public Vector3 TargetPosition { get; set; }

        public void Init()
        {
            GameBoard.Turn.OnEnemyTurnStart += OnEnemyTurnStart;
            MaxHealth = _kind.Health;
        }
        
        private async void OnEnemyTurnStart()
        {
            EffectQueue queue = GameBoard.EffectQueue;
            queue.Delay(0.3f);
            queue.AddEffect(new DealDamageEffect(0.4f, GameBoard.Player, RollAttack, false), 0);
            queue.AddEffect(new AddArmorEffect(0.2f, this, RollArmor, false), 0);
            await Task.Delay(25);
            queue.AddEffect(new TriggerPereodicDamageEffect(0.2f, this), 0);
            await Task.Delay(25);
            queue.AddEffect(new ClearArmorEffect( 0.1f, this), 0);
        }

        private void FixedUpdate()
        {
            const float MovementSpeed = 10;
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, Time.fixedDeltaTime * MovementSpeed);
        }

        private void OnDestroy()
        {
            GameBoard.Turn.OnEnemyTurnStart -= OnEnemyTurnStart;
        }
    }
}