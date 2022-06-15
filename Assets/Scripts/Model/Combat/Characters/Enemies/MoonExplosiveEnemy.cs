using System;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class MoonExplosiveEnemy : Enemy
    {
        [SerializeField] private int _explosionDamage;
        
        private void Awake()
        {
            OnDeath += Explode;
        }

        private void Explode()
        {
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, GameBoard.Player, _explosionDamage, false), 0);
        }
    }
}