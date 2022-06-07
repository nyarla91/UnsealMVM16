using System;
using System.Collections.Generic;
using System.Linq;
using Essentials;
using Model.Cards;
using Model.Cards.Combat;
using Model.Cards.Spells;
using Model.Cards.Spells.Nature;
using Model.Combat.Effects;

namespace Model.Combat.Shapeshifting
{
    public class NatureForm : Form
    {
        private List<Type> _growths = new[] { typeof(GrowthSpikesSpell), typeof(GrowthBlossomSpell), typeof(GrowthRootsSpell)}.ToList();
        
        public override void Enter()
        {
            base.Enter();
            for (int i = 0; i < 2; i++)
            {
                GameBoard.EffectQueue.AddEffect(
                    new AddSpellToAreaEffect<CardInDeck>(0.05f, GameBoard.PlayerDeck, _growths.PickRandomElement()), 0);
            }
            GameBoard.PlayerHand.ForbiddenType = SpellType.Blood;
        }

        public override void Exit()
        {
            base.Exit();
            GameBoard.PlayerHand.ForbiddenType = SpellType.None;
        }
    }
}