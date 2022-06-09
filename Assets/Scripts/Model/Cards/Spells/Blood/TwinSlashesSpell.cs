using System.Collections.Generic;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class TwinSlashesSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Twin slashes",
            "Двойной удар"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to two enemies.",
            "Наносит 2<dm> двум противникам"
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
            List<Enemy> targets = await GetTargets<Enemy>(Choose2EnemiesMessage, 2, true);
            if (targets == null)
                return;
            foreach (Enemy target in targets)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, Burst), 0);
            }
        }
    }
}