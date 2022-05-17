using System.Collections.Generic;
using Model.Combat.Actions;
using Model.Combat.Characters;

namespace Model.Combat.Cards.Spells
{
    public class RoughWoundSpell : Spell
    {
        public override async void OnPlay()
        {
            List<Character> characters =
                await GameBoard.TargetChooser.StartTargetsChoose<Character>(3, true);

            foreach (var character in characters)
            {
                GameBoard.EffectQueue.AddEffect(new DamageEffect(character, 2, 0.1f), false);
            }
        }
    }
}