using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class FragnanceSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Fragnance",
            "Аромат"
        );
        public override LocalizedString Description => new LocalizedString(
            "Draw 6<cr>.\nExile this <cr>",
            "Возьмите 6<cr>\nОчистите эту <cr>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            for (int i = 0; i < 6; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
            }
            await GameBoard.EffectQueue.WaitForEffects();
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace));
        }
    }
}