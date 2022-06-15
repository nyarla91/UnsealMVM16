using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class AntidoteSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Antidote",
            "Противоядие"
        );
        public override LocalizedString Description => new LocalizedString(
            "Cure 2<tx>",
            "Вылечите 2<tx>"
        );
        public override SpellType Type => SpellType.Action;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new CureIntoxicationEffect(0.1f, GameBoard.Player, 2));
        }
    }
}