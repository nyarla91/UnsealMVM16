using System.Collections.Generic;
using Essentials.Sound;
using Model.Combat.Effects;
using UnityEngine;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Cards.Combat
{
    public sealed class CardInHand : CardInCombat
    {
        private static readonly string[] PlaySounds = {"Card/Play1", "Card/Play2", "Card/Play3", "Card/Play4"};
        private static readonly string[] DiscardSounds = {"Card/Discard1", "Card/Discard2", "Card/Discard3", "Card/Discard4"};
        public override bool ShowPlayableOutline => Playable;

        private bool Playable => !Pause.IsPaused && !GameBoard.TargetChooser.ChooseActive && !GameBoard.EffectQueue.EffectInProgress && 
            !GameBoard.PlayerBoard.IsFull && Spell.PlayRequirements.Invoke() && GameBoard.PlayerHand.ForbiddenType != Spell.Type;
        
        [DontCallFromSpells]
        public void Discard()
        {
            Spell.OnDiscard();
            AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(PlaySounds), 1);
            GameBoard.PlayerHand.OnSpellDiscarded?.Invoke(Spell);
            TransformIntoCardInAnotherArea<CardInDiscardPile>();
        }


        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerHand.RemoveCard(this);
        }

        public override void Init()
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
            
            AudioSource.PlayOneShot(SoundRandomizer.LoadAudio(PlaySounds), 1);
            GameBoard.Turn.AddCardPlayed();
            GameBoard.PlayerHand.OnSpellPlayed?.Invoke(Spell);
            Spell.OnPlay(GameBoard.PlayerBoard.TrySpendBurst());
            TransformIntoCardInAnotherArea<CardOnBoard>();
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}