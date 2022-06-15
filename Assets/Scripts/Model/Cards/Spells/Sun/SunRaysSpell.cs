using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Sun
{
    public class SunRaysSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sun rays",
            "Солнечные лучи"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the start of your turn gain 1<br>",
            "[Пассивно:] В начал хода получите 1<br>"
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnStart += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 1), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnStart -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}