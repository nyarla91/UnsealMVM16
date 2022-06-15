using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class EncryptingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Encrypting",
            "Расшифровка"
        );
        public override LocalizedString Description => new LocalizedString(
            "Move all <ch> from this <cr> to another.\n[Await:] At the end of your turn add 1<ch>.",
            "Переместите все <cr> с этйо <cr> на другую.\n[Ожидание:] В конце хода добавьте 1<ch>."
        );
        public override SpellType Type => SpellType.Action;

        public override void OnDraw()
        {
            base.OnDraw();
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd()
        {
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1));
        }

        public override void OnDiscard()
        {
            base.OnDiscard();
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
        }

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            CardOnBoard cardOnBoard = await GetTarget<CardOnBoard>(ChooseCardMessage, true); 
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, cardOnBoard.Spell, Charges));
            Charges = 0;
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
        }
    }
}