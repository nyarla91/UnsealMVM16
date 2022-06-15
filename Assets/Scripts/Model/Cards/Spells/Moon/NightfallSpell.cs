using System.Threading.Tasks;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Moon
{
    public class NightfallSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Nightfall",
            "Падение ночи"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn you may purge a <cr>",
            "[Пассиво:] В конце хода вы можете очистить <cr>"
        );
        public override SpellType Type => SpellType.Moon;
        public override bool HasPassive => true;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private async void OnPlayerTurnEnd()
        {
            print("StartTargeting");
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                CardOnBoard target = await GetTarget<CardOnBoard>(ChooseCardToPurgeMessage, false);
                if (target == null)
                    return;
                GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, target), 0);
            }
        }

        public override async void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}