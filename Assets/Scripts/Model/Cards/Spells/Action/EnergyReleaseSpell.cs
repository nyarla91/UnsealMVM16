using Model.Cards.Combat;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class EnergyReleaseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Energy release",
            "Высвобождение энергии"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\nAdd 2<ch> to another card.",
            "Наносит 2<dm> противнику.\nДобавьте 2<ch> на другую карту."
        );
        public override SpellType Type => SpellType.Action;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, Burst));
            await GameBoard.EffectQueue.WaitForEffects();
            CardOnBoard cardOnBoard = await GetTarget<CardOnBoard>(ChooseCardMessage, true);
            GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, cardOnBoard.Spell, 2));
        }
    }
}