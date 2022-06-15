using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class AlignmentSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Alignment",
            "Сосредоточие"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Purge:] Deal 2<dm> to an enemy for each <ch>.",
            "[Очищение:] Наносит 2<dm> противнику за каждый <ch>."
        );
        public override SpellType Type => SpellType.Action;

        public override async void OnPurge()
        {
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2 * Charges, Burst), 0);
            base.OnPurge();
        }
    }
}