using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Moon
{
    public class OrbitalEnergySpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Orbital energy",
            "Орбитальная энергия"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\nIf your hand is empty deal 5<dm> instead",
            "Наносит 2<dm> противнику.\nЕсли ваша рука пуста, вместо этого наносит 5<dm>"
        );
        public override SpellType Type => SpellType.Moon;
        public override async void OnPlay(bool burst)
        {
            base.OnPurge();
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            int damage = GameBoard.PlayerHand.Size == 0 ? 5 : 2;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage: damage, burst));
        }
    }
}