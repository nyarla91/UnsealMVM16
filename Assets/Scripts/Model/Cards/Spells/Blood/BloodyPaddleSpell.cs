using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Blood
{
    public class BloodyPaddleSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bloody paddle",
            "Кровавая лужа"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn apply 2<bl> to an enemy",
            "[Пассивно:] В конце хода накладывает 2<bl> на противника."
        );

        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private async void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
                if (target == null)
                    return;
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 2, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}