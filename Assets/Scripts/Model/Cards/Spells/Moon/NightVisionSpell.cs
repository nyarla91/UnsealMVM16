using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class NightVisionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Night vision",
            "Ночное видение"
        );
        public override LocalizedString Description => new LocalizedString(
            "Draw <cr> up to 5. Gain 1<ap> for each <cr> drawn",
            "Доберите <cr> до 5. Получите 1<ap> за каждую взятую <cr>"
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int cardsToDraw = 6 - GameBoard.PlayerHand.Size;
            if (cardsToDraw <= 0)
                return;
            for (int i = 0; i < cardsToDraw; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.05f, GameBoard.PlayerDeck));
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 1, Burst));
            }
        }
    }
}