using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class SpikedWartSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Spiked warts",
            "Шипастые наросты"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 5<dm> to a character then give it 10<ap>",
            "Наносит 5<dm> урона персонажу, затем он получает 10<ap>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Character target = await GetTarget<Character>(ChooseEnemyMessage, false);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 5, burst), 0);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, target, 10, burst), 1);
        }
    }
}