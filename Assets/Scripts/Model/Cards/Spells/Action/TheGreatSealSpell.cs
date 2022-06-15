using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class TheGreatSealSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "The Great Seal",
            "Великая Печать"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Add 1<ch> to another <cr> on the board",
            "[Действие:] Добавьте 1<ch> to another <cr> на поле"
        );
        public override SpellType Type => SpellType.Action;

        public override bool HasAction => true;

        public override async void OnUseAction()
        {
            base.OnUseAction();
            CardOnBoard cardOnBoard = await GetTarget<CardOnBoard>(ChooseCardMessage, true); 
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, cardOnBoard.Spell, 1));
        }
    }
}