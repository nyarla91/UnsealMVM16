using System;
using System.Threading.Tasks;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class EndureThePainSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Endure the pain",
            "Терпеть боль"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap>\n[Purge:] Restore all <hp> you lost this turn.",
            "Получите 2<ap>\n[Очищение:]Восстановите всё <hp>, которое вы потеряли на это ходу"
        );
        public override SpellType Type => SpellType.Blood;

        private int _healthLost;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, burst));
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, _healthLost, Burst), 0);
        }

        private async void Start()
        {
            await Task.Delay(100);
            GameBoard.Player.OnLoseHealth += AddLostHealth;
            GameBoard.Turn.OnPlayerTurnStart += DiscardHealthLost;
        }

        private void AddLostHealth(int health) => _healthLost += health;
        private void DiscardHealthLost() => _healthLost = 0;

        private void OnDestroy()
        {
            GameBoard.Player.OnLoseHealth -= AddLostHealth;
            GameBoard.Turn.OnPlayerTurnStart -= DiscardHealthLost;
        }
    }
}