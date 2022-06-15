using System.Threading.Tasks;
using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
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
        public override SpellType Type => SpellType.Moon;
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.2f, target, 1, burst));
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck));
            await Task.Delay(1);
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, (CardOnBoard) CardPlace));
        }
    }
}