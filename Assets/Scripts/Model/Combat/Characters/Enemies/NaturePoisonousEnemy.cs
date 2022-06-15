using Model.Cards;
using Model.Cards.Combat;
using Model.Cards.Spells.Nature;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Combat.Characters.Enemies
{
    public class NaturePoisonousEnemy : Enemy
    {
        [SerializeField] private int _venomsAdded;
        
        protected override void BeforeActivation()
        {
            base.BeforeActivation();
            for (int i = 0; i < _venomsAdded; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(VenomSpell)), 0);
            }
        }
    }
}