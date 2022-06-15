using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class BloodRainSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blood rain",
            "Кровавый дождь"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Purge:] Apply 3<bl> to enemy for each <ch>",
            "[Очищение:] Накладывает 3<bl> на противника за каждый <ch>"
        );
        
        public override SpellType Type => SpellType.Blood;

        public override async void OnPurge()
        {
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            for (int i = 0; i < Charges; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, Charges, Burst), 0);
            }
            base.OnPurge();
        }
    }
}