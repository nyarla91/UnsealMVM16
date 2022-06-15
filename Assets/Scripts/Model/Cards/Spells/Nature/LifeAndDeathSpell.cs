using System.Threading.Tasks;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class LifeAndDeathSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Life and death",
            "Жизнь и смерть"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 6<dm> to an enemy. Restore any extra <dm> as <hp>\nExile this <cr>",
            "Наносит 6<dm> противнику. Восстановите любой лишний <dm> как <hp>\nИзгоните эту <cr>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            int healthRestored = 6 - target.Armor - target.Health;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 6, burst));
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, healthRestored, burst));
            await GameBoard.EffectQueue.WaitForEffects();
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace));
        }
    }
}