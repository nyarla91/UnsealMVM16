using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class StarShieldSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Star shield",
            "Звёздный щит"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn you may discard a <cr> to gain 3<ap>",
            "[Пассивно:] В конце хода вы можете сбрость <cr>, чтобы получить 3<ap>"
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private async void OnPlayerTurnEnd()
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                CardInHand cardToDiscard = await GetTarget<CardInHand>(ChooseCardToDiscardMessage, false);
                if (cardToDiscard == null)
                    return;
            
                GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.1f, cardToDiscard), 0);
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 3, Burst), 1);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}