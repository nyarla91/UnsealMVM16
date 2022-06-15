using System.Threading.Tasks;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class CalmSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Calm",
            "Затишье"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap>.\n if this is the only <cr> on board purge this <cr> and draw a <cr>",
            "Получите 2<ap>.\nЕсли это единственная <cr> на поле, очистите эту <cr> и возьмите <cr>"
        );
        public override SpellType Type => SpellType.Moon;
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.2f, GameBoard.Player, 2, burst));
            await Task.Delay(10);
            if (GameBoard.PlayerBoard.Size != 1) 
                return;
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, (CardOnBoard) CardPlace));
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
        }
    }
}