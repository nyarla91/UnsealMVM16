using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class HeartbeatSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Heartbeat",
            "Сердцебиение"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you lose <hp>, restore 1 <hp>",
            "[Пассивно:] Когда вы теряете <hp>, восстановите 1 <hp>"
        );
        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        private int _healthLost;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Player.OnLoseHealth += OnLoseHealth;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, burst));
        }

        private void OnLoseHealth(int health)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 1, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.Player.OnLoseHealth -= OnLoseHealth;
        }
    }
}