using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class IncenerationSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Inceneration",
            "Испепеление"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\n[Burst:] Deal <dm> to all enemies instead",
            "Наносит 2<dm> противнику.\n[Вспышка:] Вместо этого наносит <dm> всем противникам."
        );
        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            if (burst)
            {
                foreach (Enemy enemy in GameBoard.EnemyPool.ActiveEnemies)
                {
                    GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, enemy, 2, true));
                }
                return;
            }
            
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, target, 2, false));
        }
    }
}