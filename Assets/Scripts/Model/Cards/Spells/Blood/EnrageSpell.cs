using Essentials;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class EnrageSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bloody paddle",
            "Кровавая лужа"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you play a <cr> lose 2<hp> and apply 3<bl> to a random enemy",
            "[Пассивно:] Каждый раз, когда вы разыграваете <cr>, потеряйте 2<hp> и наложите 3<bl> на случайного противнику"
        );

        public override SpellType Type => SpellType.Blood;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellPlayed += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd(Spell spell)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new LoseHealthEffect(0.1f, GameBoard.Player, 1), 0);
                Enemy target = GameBoard.EnemyPool.ActiveEnemies.PickRandomElement();
                if (target == null)
                    continue;
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 2, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellPlayed -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}