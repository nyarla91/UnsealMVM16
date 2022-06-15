using Model.Combat.Effects;
using Model.Combat.Shapeshifting;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class AscentionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Ascention",
            "Возвышение"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you change form gain 3<ap>",
            "[Пассивно:] Когда вы меняете облик получайте 3<ap>"
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnFormChanged += OnFormChanged;
        }

        private void OnFormChanged(Form form)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 3, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnFormChanged -= OnFormChanged;
            base.OnPurge();
        }
    }
}