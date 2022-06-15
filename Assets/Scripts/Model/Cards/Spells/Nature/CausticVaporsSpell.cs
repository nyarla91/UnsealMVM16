using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class CausticVaporsSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Entagling roots",
            "Опутывающие корни"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenver you cure <tx> deal 2<dm> to all enemies",
            "[Пассивно:] Когда вы излечиваете <tx> наносите 2<dm> всем противникам."
        );

        public override SpellType Type => SpellType.Nature;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Player.OnIntoxicationCured += OnIntoxicationCured;
        }

        private void OnIntoxicationCured(int intoxication)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier * intoxication; i++)
            {
                foreach (Enemy target in GameBoard.EnemyPool.ActiveEnemies)
                {
                    GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, target, 2, Burst), 0);
                }
            }
        }

        public override void OnPurge()
        {
            GameBoard.Player.OnIntoxicationCured -= OnIntoxicationCured;
            base.OnPurge();
        }
    }
}