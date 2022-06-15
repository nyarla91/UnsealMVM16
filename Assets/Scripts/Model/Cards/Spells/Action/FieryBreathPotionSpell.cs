using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class FieryBreathPotionSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Fiery breath potion",
            "Зелья огненного дыхания"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 1<tx>.\nDeal 7<dm>",
            "Получите 1<tx>.\nGain 7<dm>"
        );
        public override SpellType Type => SpellType.Action;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddIntoxicationEffect(0.1f, GameBoard.Player, 1));
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 7, Burst));
        }
    }
}