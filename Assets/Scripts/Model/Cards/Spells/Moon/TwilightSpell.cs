using Essentials;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class TwilightSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Twilight",
            "Сумерки"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you purge a <cr> during your turn deal 2 <dm> to a random enemy",
            "[Пассивно:] Когда вы очищаете <cr> в ваш ход наносит 2<dm> случайному противнику."
        );
        public override SpellType Type => SpellType.Moon;
        public override bool HasPassive => true;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerBoard.OnSpellPurged += OnSpellPurged;
        }

        private async void OnSpellPurged(Spell spell)
        {
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                Enemy target = GameBoard.EnemyPool.ActiveEnemies.PickRandomElement();
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.PlayerBoard.OnSpellPurged -= OnSpellPurged;
        }
    }
}