using System;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class SunSpikedEnemy : Enemy
    {
        private void Awake()
        {
            OnTakeDamage += SpikeEffect;
        }

        private void SpikeEffect(int damage)
        {
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, GameBoard.Player, 1, false), 0);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnTakeDamage -= SpikeEffect;
        }
    }
}