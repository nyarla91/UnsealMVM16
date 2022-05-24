using System.Threading.Tasks;
using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards
{
    public sealed class CardInHand : Card
    {
        [SerializeField] private GameObject _playableOutline;

        public override bool ShowPlayableOutline => Playable;

        private bool Playable => !GameBoard.TargetChooser.ChooseActive && !GameBoard.EffectQueue.EffectInProgress &&
                                 !GameBoard.PlayerBoard.IsFull && Spell.Requirements.Invoke();
        
        [DontCallFromSpells]
        public void Discard()
        {
            Spell.OnDiscard();
            MoveToDiscardPile();
        }


        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerHand.RemoveCard(this);
        }

        private void Start()
        {
            GameBoard.PlayerHand.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button == PointerType.Left)
                Play();
        }

        private void Play()
        {
            if (!Playable)
                return;
            
            GameBoard.Turn.AddCardPlayed();
            GameBoard.PlayerHand.OnSpellPlayed?.Invoke(Spell);
            Spell.OnPlay(GameBoard.PlayerBoard.TrySpendGrowth());
            MoveToBoard();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}