using System.Linq;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class LightningSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Lightning",
            "Молния"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\nDeal 5<dm> more for each card in your hand you cannot play due to form",
            "Наносит 2<dm> противнику.\nНаносит на 5<dm> больше за каждую карту в вашей руке, которую вы не можете сыграть из-за облика."
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            int damage = 2 + GameBoard.PlayerHand.Cards.Sum(card => card.Spell.Type == GameBoard.PlayerHand.ForbiddenType ? 5 : 0);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage, burst));
        }
    }
}