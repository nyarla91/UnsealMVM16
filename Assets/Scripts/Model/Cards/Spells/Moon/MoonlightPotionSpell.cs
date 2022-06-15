using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class MoonlightPotionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Moonlight potion",
            "Зелье лунного сияния"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nGain 4<ap> for each <tx>",
            "Получите 1<tx.\nПолучите 4<ap> за кадую <tx>"
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            await GameBoard.EffectQueue.WaitForEffects();
            int armor = GameBoard.Player.Intoxication * 4;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, armor, Burst));
        }
    }
}