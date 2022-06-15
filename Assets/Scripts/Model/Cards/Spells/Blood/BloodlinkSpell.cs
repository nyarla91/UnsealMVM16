using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class BloodlinkSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bloodlink",
            "Узы крови"
        );

        public override LocalizedString Description => new LocalizedString(
            "Choose an enemy.\n[Passive:] Whenever you lose <hp> on your turn deal that much <dm> to this enemy",
            "Выберите противника.'n[Пассивно:] Каждый раз, когда вы теряете <hp> на своём ходу наносите столько же <dm> этому протианику"
        );

        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        private Enemy _target;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            _target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.Player.OnLoseHealth += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd(int healthLost)
        {
            if (_target == null)
                return;
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, _target, healthLost, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Player.OnLoseHealth -= OnPlayerTurnEnd;
            _target = null;
            base.OnPurge();
        }
    }
}