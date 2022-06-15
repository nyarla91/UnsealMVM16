using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class MetabolicBoostSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Metabolic boost",
            "Ускореный метаболизм"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap> for each <tx>",
            "Получите 2<ap> за каждую <tx>"
        );
        public override SpellType Type => SpellType.Action;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int armor = GameBoard.Player.Intoxication * 2;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, armor, burst));
        }
    }
}