using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class RendSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Rend",
            "Разорвать"
        );
        public override LocalizedString Description => new LocalizedString(
            "Apply 6<bl> to an enemy",
            "Накладывает 6<bl> на противника"
        );
        public override SpellType Type => SpellType.Blood;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealPereodicDamageEffect(0.1f, target, 6, burst));
        }
    }
}