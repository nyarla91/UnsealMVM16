using Model.Combat.Effects;
using PointerType = Essentials.Pointers.PointerType;
using Vector3 = UnityEngine.Vector3;

namespace Model.Cards.Combat
{
    public sealed class CardOnBoard : CardInCombat
    {
        private static readonly string[] PurgeSound = {"Card/Discard1", "Card/Discard2", "Card/Discard3", "Card/Discard4"};
        public override bool ShowPlayableOutline => ActionAvailbale;

        private bool ActionAvailbale => !Pause.IsPaused && !GameBoard.TargetChooser.ChooseActive && !GameBoard.EffectQueue.EffectInProgress
            && Spell.HasAction && Spell.ActionAvailbale;

        [DontCallFromSpells]
        public void Purge()
        {
            Spell.OnPurge();
            AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(PurgeSound), 1);
            GameBoard.PlayerBoard.OnSpellPurged?.Invoke(Spell);
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