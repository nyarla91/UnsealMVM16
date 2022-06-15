using System;
using System.Threading.Tasks;
using Model.Combat.Effects;
using UnityEngine;
using View.Cards;
using Random = UnityEngine.Random;

namespace Model.Combat.Characters.Enemies
{
    public class Enemy : Character
    {
        [SerializeField] private int _attackPerTurn;
        [SerializeField] private int _armorPerTurn;
        [SerializeField] private bool _clearArmorOnTurn = true;

        public int AttackPerTurn => _attackPerTurn;
        public int ArmorPerTurn => _armorPerTurn;

        protected int DamageBonus { get; set; }
        
        public Vector3 TargetPosition { get; set; }

        public AbilitiyTooltip Tooltip { get; set; }

        public Action OnActivation;
        public Action OnDeactivation;

        public void Init()
        {
            GameBoard.Turn.OnEnemyTurnStart += OnEnemyTurnStart;
        }
        
        private async void OnEnemyTurnStart()
        {
            EffectQueue queue = GameBoard.EffectQueue;
            queue.Delay(0.5f);
            AfterActivation();
            queue.AddEffect(new DeactivateEnemyEffect(0.1f, this), 0);
            queue.AddEffect(new DealDamageEffect(0.4f, GameBoard.Player, AttackPerTurn + DamageBonus, false), 0);
            queue.AddEffect(new AddArmorEffect(0.2f, this, ArmorPerTurn, false), 0);
            queue.AddEffect(new ActivateEnemyEffect(0.1f, this), 0);
            
            await Task.Delay(10);
            queue.AddEffect(new TriggerPereodicDamageEffect(0.2f, this), 0);
            if (_clearArmorOnTurn)
            {
                await Task.Delay(10);
                queue.AddEffect(new ClearArmorEffect( 0.1f, this), 0);
            }
            BeforeActivation();
        }

        protected virtual void BeforeActivation() { }
        protected virtual void AfterActivation() { }

        private void FixedUpdate()
        {
            const float MovementSpeed = 10;
            transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, Time.fixedDeltaTime * MovementSpeed);
        }

        protected virtual void OnDestroy()
        {
            GameBoard.Turn.OnEnemyTurnStart -= OnEnemyTurnStart;
        }
    }
}