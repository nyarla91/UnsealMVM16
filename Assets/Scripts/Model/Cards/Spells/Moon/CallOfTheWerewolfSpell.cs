using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Combat.Shapeshifting;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class CallOfTheWerewolfSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Call of the werewolf",
            "Зов оборотня"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] When you change form purge all <cr>",
            "[Пассивно:] Когда вы меняете форму очистите все <cr>"
        );
        public override SpellType Type => SpellType.Moon;
        public override bool HasPassive => true;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.Turn.OnFormChanged += OnFormChanged;
        }
        
        private async void OnFormChanged(Form form)
        {
            foreach (CardOnBoard card in GameBoard.PlayerBoard.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.05f, card));
            }
        }

        public override async void OnPurge()
        {
            GameBoard.Turn.OnFormChanged -= OnFormChanged;
            base.OnPurge();
        }
    }
}