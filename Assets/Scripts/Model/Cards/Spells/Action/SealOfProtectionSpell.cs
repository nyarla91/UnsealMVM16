using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class SealOfProtectionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seal of Protection",
            "Печать Защиты"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Add 1<ch>.\n[Purge:] Gain 1<ap> for each <ch>.",
            "[Действие:] Добавьте 1<ch>\n[Очищение:] Получите 1<ap> за каждый <ch>"
        );
        
        public override SpellType Type => SpellType.Blood;

        public override bool HasAction => true;

        public override void OnUseAction()
        {
            base.OnUseAction();
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1));
        }

        public override void OnPurge()
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, Charges, Burst), 0);
            base.OnPurge();
        }
    }
}