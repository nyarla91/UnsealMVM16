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
            "Restore 1<hp> to yourself.\nGain 1<gr>",
            "Восстанавливает вам 1<hp>\nПолучите 1<gr>"
        );
        public override SpellType Type => SpellType.Blood;

        public override void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 1, growth));
            GameBoard.EffectQueue.AddEffect(new AddGrowthEffect(0.1f, GameBoard.PlayerBoard, 1));
        }
    }
}