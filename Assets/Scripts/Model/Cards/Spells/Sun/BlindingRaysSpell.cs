using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class BlindingRaysSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blinding rays",
            "Слепящие лучи"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Purge:] Deal 1<dm> to all enemies and gain 2<ap> for each <ch>",
            "[Очищение:] Наносит 1<dm> всем противникам и полчите ц<ap> за каждый <ch>"
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPurge()
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0, GameBoard.Player, Charges * 2, Burst), 0);
            foreach (var target in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0, target, Charges, Burst), 1);
            }
            base.OnPurge();
        }
    }
}