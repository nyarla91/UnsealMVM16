using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class VanishSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Vanish",
            "Исчезнуть"
        );
        public override LocalizedString Description => new LocalizedString(
            "Double your <ap>",
            "Удваивает вашу <ap>"
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, GameBoard.Player.Armor, burst));
        }
    }
}