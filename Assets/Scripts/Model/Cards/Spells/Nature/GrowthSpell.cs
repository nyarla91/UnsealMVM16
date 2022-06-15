using System;
using System.Threading.Tasks;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public abstract class GrowthSpell : Spell
    {
        public override SpellType Type => SpellType.Nature;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDraw()
        {
            base.OnDraw();
            GameBoard.EffectQueue.AddEffect(new WaitEffect(0.5f));
            await PerformGrowthSpecificEffect(0);
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace), 1);
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 2);
        }

        protected abstract Task PerformGrowthSpecificEffect(int index);
    }
}