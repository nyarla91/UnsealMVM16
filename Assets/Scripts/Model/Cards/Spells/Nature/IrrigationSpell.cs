using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class IrrigationSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Irrigation",
            "Орошение"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you exile a <cr> restore 2<hp>",
            "[Пассивно:] Когда вы изгоняете <cr> восстанавливаете 2<hp>"
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellExiled += OnSpellExiled;
        }

        private async void OnSpellExiled(Spell spell)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 2, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellExiled -= OnSpellExiled;
            base.OnPurge();
        }
    }
}