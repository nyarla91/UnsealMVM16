using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class LunarFlashSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Lunar flash",
            "Лунная вспышка"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 1<dm>\nDraw a <cr>\nPurge this <cr>",
            "Наносит 1<dm>\nВозьмите <cr>\nОчистите эту <cr>"
        );
        public override SpellType Type => SpellType.None;
        public override async void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            Character target = await GetTarget<Character>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.2f, target, 1, growth));
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, GetComponent<CardOnBoard>()));
        }
    }
}