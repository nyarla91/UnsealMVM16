using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class HolyWaterSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Holy water",
            "Святая вода"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] If your hand is empty at the end of your turn cure 1<tx>",
            "[Пассивно:] В конце хода если ваша рука пуста вылечите 1<tx>"
        );
        
        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd()
        {
            if (GameBoard.PlayerHand.Size > 0)
                return;
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new CureIntoxicationEffect(0.1f, GameBoard.Player, 1));
            }
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
        }
    }
}