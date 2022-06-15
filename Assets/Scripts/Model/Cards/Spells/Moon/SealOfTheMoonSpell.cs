using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class SealOfTheMoonSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seal of the Moon",
            "Печать Луны"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Add 1<ch>.\n[Passive:] Ready action every time your purge a <cr> on your turn.\n[Purge:] if its your turn gain 1<ap> for each<ch>.",
            "[Действие:] Добавьте 1<ch>\n[Пассивно:] Подготавливайте действие каждый раз, когда вы очищаете <cr> на вашем ходу.\n[Очищение:] Если сейчас ваш ход получите 1<ap> за каждый <ch>"
        );
        public override SpellType Type => SpellType.Moon;

        public override bool HasAction => true;
        public override bool HasPassive => true;

        public override void OnUseAction()
        {
            base.OnUseAction();
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1));
        }

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerBoard.OnSpellPurged += OnSpellPurged;
        }

        private void OnSpellPurged(Spell spell)
        {
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            ReadyAction();
        }

        public override void OnPurge()
        {
            GameBoard.PlayerBoard.OnSpellPurged -= OnSpellPurged;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, Charges, Burst), 0);
            base.OnPurge();
        }
    }
}