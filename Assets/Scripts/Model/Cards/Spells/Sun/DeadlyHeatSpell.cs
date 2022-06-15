using System;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Sun
{
    public class DeadlyHeatSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Deadly heat",
            "Смертельная жара"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Discard:] if its your turn deal 6<dm> to an enemy",
            "[Сброс:] Если сейчас ваш ход наносит 6<dm> противнику"
        );
        public override SpellType Type => SpellType.Sun;
        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDiscard()
        {
            base.OnDiscard();
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 6, false));
        }
    }
}