using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class RoarSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Roar",
            "Рык"
        );
        public override LocalizedString Description => new LocalizedString(
            "All enemies lose 3<ap> and take 1<dm>",
            "Все противники теряют 3<ap> и получают 1<dm>"
        );
        public override SpellType Type => SpellType.Blood;
        
        
        protected static LocalizedString Choose2EnemiesMessage => new LocalizedString
        (
            "Choose 2 enemies",
            "Выберите 2 противника"
        );

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (Enemy target in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new LoseArmorEffect(0.1f, target, 3), 0);
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 1, Burst), 1);
            }
        }
    }
}