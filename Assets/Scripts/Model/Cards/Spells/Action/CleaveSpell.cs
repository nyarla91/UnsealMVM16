﻿using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class CleaveSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Cleave",
            "Размашистый удар"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn deal 1<dm> to all enemies.",
            "[Пассивно:] В конце вашего хода наносит 1<dm> всем противникам."
        );
        public override SpellType Type => SpellType.None;


        public override void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.InsertEffect(new WaitEffect(0.1f), 0);
                foreach (var enemy in GameBoard.Enemies)
                {
                    GameBoard.EffectQueue.InsertEffect(new DealDamageEffect(0, enemy, 1, Growth), 0);
                }
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}