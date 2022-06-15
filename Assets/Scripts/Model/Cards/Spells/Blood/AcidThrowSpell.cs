using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class AcidThrowSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Acid throw",
            "Бросок кислоты"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nApply 4<bl> to an enemy twice.",
            "Получите 1<tx>.\nНакладывает 4<bl> противнику дважды."
        );
        
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            for (int i = 0; i < 2; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 4, Burst));
            }
        }
    }
}