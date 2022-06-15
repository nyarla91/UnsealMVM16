using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class BiteSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Bite",
            "Укус"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy and apply 3<bl> to it",
            "Наносит 2<dm> противнику и кладывает на него 3<bl>"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, burst));
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 3, burst));
        }
    }
}