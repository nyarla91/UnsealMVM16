using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class DefensivePotionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Defensive potion",
            "Защитное зелье"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nGain 7<ap>",
            "Получите 1<tx>.\nGain 7<ap>"
        );
        public override SpellType Type => SpellType.Action;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 7, Burst));
        }
    }
}