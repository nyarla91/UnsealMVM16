using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class ManeuverSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Maneuver",
            "Манёвр"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap>\n[Purge:] Gain 2<ap>",
            "Получите 2<ap>\n[Очищение:] Получите 2<ap>"
        );
        public override SpellType Type => SpellType.Action;
        
        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, burst));
        }

        public override void OnPurge()
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, Burst), 0);
            base.OnPurge();
        }
    }
}