using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class TotalResistanceSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Total resistance",
            "Полное сопротивление"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you discard a <cr> you cannot play due to form gain 2<ap>",
            "[Пассивно:] Когда вы сбрасываете <cr>, которую вы не можете сыграть из-за облика получайте 2<ap>"
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellDiscarded += OnSpellDiscarded;
        }

        private async void OnSpellDiscarded(Spell spell)
        {
            if (spell.Type != GameBoard.PlayerHand.ForbiddenType)
                return;
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellExiled -= OnSpellDiscarded;
            base.OnPurge();
        }
    }
}