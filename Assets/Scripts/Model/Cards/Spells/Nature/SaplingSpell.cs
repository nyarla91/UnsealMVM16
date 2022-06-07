using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class SaplingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sapling",
            "Саженец"
        );
        public override LocalizedString Description => new LocalizedString(
            "Restore 1<hp> to yourself.\nGain 1<br>",
            "Восстанавливает вам 1<hp>\nПолучите 1<br>"
        );
        public override SpellType Type => SpellType.Nature;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 1, burst));
            GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 1));
        }
    }
}