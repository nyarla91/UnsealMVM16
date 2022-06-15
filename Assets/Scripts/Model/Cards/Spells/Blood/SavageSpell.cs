using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class SavageSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Savage",
            "Дикость"
        );
        public override LocalizedString Description => new LocalizedString(
            "Apply 4<bl> to ALL characters.",
            "Накладывает 4<bl> на ВСЕХ персонажей."
        );
        public override SpellType Type => SpellType.Blood;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, GameBoard.Player, 4, Burst));
            foreach (Enemy enemy in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, enemy, 4, Burst));
            }
        }
    }
}