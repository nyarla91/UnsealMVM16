using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells
{
    public class IlluminationSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Illumination",
            "Озарение"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn gain 3<ap> if you played 3<cr> this turn or less",
            "[Пассивно:] В конце хода вы получаете 3<ap>, если разыграли вэтот ход 3<cr> или меньше."
        );

        public override SpellType Type => SpellType.Sun;

        public override void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            GameBoard.Turn.OnPlayerTurnEnd += TurnOnOnPlayerTurnEnd;
        }

        private void TurnOnOnPlayerTurnEnd()
        {
            if (GameBoard.Turn.CardsPlayedThisTurn > 3) 
                return;
            
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.InsertEffect(new AddArmorEffect(0.1f, GameBoard.Player, 3, Growth), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= TurnOnOnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}