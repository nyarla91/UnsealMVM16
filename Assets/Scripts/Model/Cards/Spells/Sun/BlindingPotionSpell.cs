using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class BlindingPotionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blinding potion",
            "Слепящее зелье"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nGain 4<br>",
            "Получите 1<tx>.\nПолучите 4<br>"
        );
        
        public override SpellType Type => SpellType.Blood;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 4));
        }
    }
}