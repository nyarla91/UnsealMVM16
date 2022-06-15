using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class SealOfTheNatureSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Seal of the Nature",
            "Печать Природы"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Action:] Add 1<ch>.\n[Purge:] Draw a <cr> for each <ch>.",
            "[Действие:] Добавьте 1<ch>\n[Очищение:] Возьмите <cr> за каждый<ch>"
        );
        
        public override SpellType Type => SpellType.Nature;

        public override bool HasAction => true;

        public override void OnUseAction()
        {
            base.OnUseAction();
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, this, 1));
        }

        public override void OnPurge()
        {
            for (int i = 0; i < Charges; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 0);
            }
            base.OnPurge();
        }
    }
}