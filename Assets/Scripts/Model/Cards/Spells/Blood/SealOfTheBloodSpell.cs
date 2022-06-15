using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class SealOfTheBloodSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seal of the Blood",
            "Печать Крови"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Lose 1<hp> and add 1<ch>.\n[Purge:] Apply X<bl> to all enemies.\n(X equals to <ch>)",
            "[Действие:] Потеряйте 1<hp> и добавьте 1<ch>\n[Очищение:] Накладывает X<bl> на всех противников.\n(X равен <ch>)"
        );
        
        public override SpellType Type => SpellType.Action;

        public override bool HasAction => true;

        public override void OnUseAction()
        {
            base.OnUseAction();
            GameBoard.EffectQueue.AddEffect(new LoseHealthEffect(0.1f, GameBoard.Player, 1));
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1));
        }

        public override void OnPurge()
        {
            foreach (Enemy enemy in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, enemy, Charges, Burst), 0);
            }
            base.OnPurge();
        }
    }
}