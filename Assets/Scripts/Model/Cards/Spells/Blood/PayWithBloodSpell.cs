using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Blood
{
    public class PayWithBloodSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Pay with blood",
            "Плата кровью"
        );

        public override LocalizedString Description => new LocalizedString(
            "Choose a <cr> on the board.\n[Passive:] Whenever you lose <hp> on your turn add 1 <ch> on that <cr>",
            "Выберите <cr> на поле.\n[Пассивно:] Каждый раз, когда вы теряете <hp> на своём ходу добавляйте 1<ch> на эту <cr>"
        );

        public override SpellType Type => SpellType.Blood;
        public override bool HasPassive => true;

        private CardOnBoard _target;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            _target = await GetTarget<CardOnBoard>(ChooseCardMessage, true);
            GameBoard.Player.OnLoseHealth += OnLoseHealth;
        }

        private void OnLoseHealth(int healthLost)
        {
            if (_target == null)
                return;
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddChargesToSpellEffect(0.1f, _target.Spell, 1), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.Player.OnLoseHealth -= OnLoseHealth;
            _target = null;
            base.OnPurge();
        }
    }
}