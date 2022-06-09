using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class IlluminationSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Illumination",
            "Озарение"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] At the end of your turn restore 1<hp> if you played 3<cr> this turn or less",
            "[Пассивно:] В конце хода восстановите 1<ap>, если разыграли вэтот ход 3<cr> или меньше."
        );

        public override SpellType Type => SpellType.Sun;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnPlayerTurnEnd += OnPlayerTurnEnd;
        }

        private void OnPlayerTurnEnd()
        {
            if (GameBoard.Turn.CardsPlayedThisTurn > 3) 
                return;
            
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 1, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Turn.OnPlayerTurnEnd -= OnPlayerTurnEnd;
            base.OnPurge();
        }
    }
}