using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class RegenerationSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Regeneration",
            "Заживление"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] When you lose 5<hp> or more at once restore 8<hp> then purge this<cr>.",
            "[Пассивно:] Когда вы теряете 5<hp> или больше за один раз восстановите 8<hp> и очистите эту <cr>."
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Player.OnLoseHealth += OnLoseHealth;
        }

        private void OnLoseHealth(int health)
        {
            if (health < 5)
                return;
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 8, Burst), 0);
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, (CardOnBoard) CardPlace), 1);
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.Player.OnLoseHealth -= OnLoseHealth;
        }
    }
}