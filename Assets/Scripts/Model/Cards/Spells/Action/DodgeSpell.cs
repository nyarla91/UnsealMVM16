using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class DodgeSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Dodge",
            "Уклонение"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 3<ap>",
            "Получите 3<ap>"
        );
        public override SpellType Type => SpellType.Action;
        public override bool InfiniteInDeck => true;

        public override void OnPlay(bool burst)
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 3, burst));
            base.OnPlay(burst);
        }
    }
}