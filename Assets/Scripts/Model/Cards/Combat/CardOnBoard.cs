using Model.Combat.Effects;
using PointerType = Essentials.Pointers.PointerType;
using Vector3 = UnityEngine.Vector3;

namespace Model.Cards.Combat
{
    public sealed class CardOnBoard : CardInCombat
    {
        
        public override bool ShowPlayableOutline => ActionAvailbale;

        private bool ActionAvailbale => !GameBoard.TargetChooser.ChooseActive && !GameBoard.EffectQueue.EffectInProgress
            && Spell.HasAction && Spell.ActionAvailbale;

        [DontCallFromSpells]
        public void Purge()
        {
            Spell.OnPurge();
            GameBoard.PlayerBoard.OnCardPurged?.Invoke(Spell);
            TransformIntoCardInAnotherArea<CardInDiscardPile>();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerBoard.RemoveCard(this);
        }

        public override void Init()
        {
            GameBoard.PlayerBoard.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (ActionAvailbale)
                Spell.OnUseAction();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}