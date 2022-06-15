using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class FulfillmentSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Fulfillment",
            "Преисполнение"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<br>\n[Discard:] if its your turn gain 2<br>",
            "Получите 2<br>\n[Сброс:] Если сейчас ваш ход получите 2<br>"
        );
        public override SpellType Type => SpellType.Sun;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 2));
        }

        public override void OnDiscard()
        {
            base.OnDiscard();
            if (GameBoard.Turn.IsPlayerTurn)
                GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 2));
        }
    }
}