using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class BloodRazorEnemy : Enemy
    {
        [SerializeField] private int _bleed;
        
        protected override void AfterActivation()
        {
            base.AfterActivation();
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, GameBoard.Player, _bleed, false), 0);
        }
    }
}