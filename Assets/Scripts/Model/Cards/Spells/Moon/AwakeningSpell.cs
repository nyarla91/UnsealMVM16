using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class AwakeningSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Awakening",
            "Пробуждение"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 1<ch> to all <cr> on the board and in your hand.\nPurge this <cr>.",
            "Добавьте 1<ch> на все <cr> на поле и в вашей руке.\nОчистите эту <cr>."
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (CardOnBoard card in GameBoard.PlayerBoard.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.05f, card.Spell, 1));
            }
            foreach (CardInHand card in GameBoard.PlayerHand.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.05f, card.Spell, 1));
            }
        }
    }
}