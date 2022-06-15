using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Action
{
    public class HealingPotionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Healing potion",
            "Лечебное зелье"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nRestore 5<hp>",
            "Получите 1<tx>.\nВосстановите 5<hp>"
        );
        public override SpellType Type => SpellType.Action;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 5, Burst));
        }
    }
}