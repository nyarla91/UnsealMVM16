using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class BloodVialSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blood vial",
            "Пузырёк крови"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<tx>.\n[Passive:] Whenever you gain <tx> restore 2<hp>",
            "Получите 2<tx>.\n[Пассивно:] Когда вы получаете <tx> восстанавливейте 2<hp>"
        );
        
        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Player.OnIntoxicationAdded += OnIntoxicationAdded;
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 2));
        }

        private void OnIntoxicationAdded(int intoxication)
        {
            for (int i = 0; i < intoxication * GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 2, Burst), 0);
            }
        }

        public override async void OnPurge()
        {
            base.OnPurge();
            GameBoard.Player.OnIntoxicationAdded -= OnIntoxicationAdded;
        }
    }
}