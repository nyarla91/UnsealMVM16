using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class UnsealSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Unseal",
            "Распечатать"
        );
        public override LocalizedString Description => new LocalizedString(
            "Ready all actions.\nDraw a <cr>",
            "Подготовьте все действия.\nВозьмите <cr>"
        );
        public override SpellType Type => SpellType.Action;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (CardOnBoard card in GameBoard.PlayerBoard.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new ReadyActionEffect(0.1f, card));
            }
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
        }
    }
}