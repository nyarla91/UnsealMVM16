using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class MoonBossEnemy : Enemy
    {
        [SerializeField] private int _cardsRequired;
        [SerializeField] private int _healthRestored;

        protected override void BeforeActivation()
        {
            base.BeforeActivation();
            if (GameBoard.Turn.CardsPlayedThisTurn < _cardsRequired)
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, this, _healthRestored, false), 0);
        }
    }
}