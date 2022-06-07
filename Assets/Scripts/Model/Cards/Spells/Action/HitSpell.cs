using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class HitSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Hit",
            "Удар"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 3<dm>",
            "Наносит 3<dm>"
        );
        public override SpellType Type => SpellType.Action;
        public override bool InfiniteInDeck => true;

        public override async void OnPlay(bool burst)
        {
            Character target = await GetTarget<Enemy>(ChooseEnemyMessage,true);
            if (target != null)
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 3, burst));
            base.OnPlay(burst);
        }
    }
}