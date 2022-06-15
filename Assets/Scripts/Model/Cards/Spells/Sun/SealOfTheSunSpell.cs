using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class SealOfTheSunSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seal of the Sun",
            "Печать Солнца"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn add 1<ch>.\n[Purge:] Gain 1<br> for each <ch>",
            "[Пассивно:] В конце хода добавьте 1<ch>.\n[Очищение:] Получите 1<br> за каждый <ch>"
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
                GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnStart -= OnPlayerTurnEnd;
            GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, Charges), 0);
            base.OnPurge();
        }
    }
}