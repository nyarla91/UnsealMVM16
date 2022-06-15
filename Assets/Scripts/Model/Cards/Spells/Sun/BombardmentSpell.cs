using Essentials;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class BombardmentSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bombardment",
            "Бомбардировка"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you discard a <cr> during your turn deal 3<dm> to a random enemy.",
            "[Пассивно:] Когда вы сбрасываете <cr> в ваш ход нанесите 3<dm. случайному противнику."
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellDiscarded += OnSpellDiscarded;
        }

        private async void OnSpellDiscarded(Spell spell)
        {
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                Enemy target = GameBoard.EnemyPool.ActiveEnemies.PickRandomElement();
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 3, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellDiscarded -= OnSpellDiscarded;
            base.OnPurge();
        }
    }
}