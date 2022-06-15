using System.Linq;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class ApocalypseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Apocalypse",
            "Апокалипсис"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 1<dm> to an enemy for each <ch> on all your cards",
            "Наносит 1<dm> противнику за каждый <ch> на всех ваших картах."
        );
        public override SpellType Type => SpellType.Sun;
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int damage = GameBoard.PlayerHand.Cards.Sum(card => card.Spell.Charges) + 
                         GameBoard.PlayerBoard.Cards.Sum(card => card.Spell.Charges);

            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage, Burst));
        }
    }
}