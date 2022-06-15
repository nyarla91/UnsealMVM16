using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Essentials;
using Model.Combat.GameAreas;
using Model.Localization;
using UnityEngine;
using Zenject;

namespace Model.Cards.Spells
{
    public abstract class Spell : Transformer, ICardPlaceChangedHandler
    {
        private int _charges;
        private Material _icon;
        [Inject] private GameBoard _gameBoard;
        
        public abstract LocalizedString Name { get; }
        public abstract LocalizedString Description { get; }
        public abstract SpellType Type { get; }
        public virtual Func<bool> PlayRequirements => () => true;
        public virtual bool InfiniteInDeck => false;
        public virtual bool HasPassive => false;
        public virtual bool HasAction => false;
        public bool ActionAvailbale { get; private set; } = true;
        public Material Icon => _icon ??= Resources.Load<Material>("SpellIcons/" + Name.Eng);
        protected static LocalizedString ChooseEnemyMessage => new LocalizedString
        (
            "Choose enemy",
            "Выберите противника"
        );

        protected static LocalizedString ChooseCardMessage => new LocalizedString
        (
            "Choose card",
            "Выберите карту"
        );

        protected static LocalizedString ChooseCharacterMessage => new LocalizedString
        (
            "Choose character",
            "Выберите персонажа"
        );

        protected static LocalizedString ChooseCardToDiscardMessage => new LocalizedString
        (
            "Choose card to discard",
            "Выберите карту, которую сбросите"
        );

        protected static LocalizedString ChooseCardToPurgeMessage => new LocalizedString
        (
            "Choose card to purge",
            "Выберите карту, которую очистите"
        );

        
        public GameBoard GameBoard
        {
            protected get { return _gameBoard; }
            set
            {
                _gameBoard = value;
                if (HasAction)
                    GameBoard.Turn.OnPlayerTurnStart += RestoreAction;
            }
        }

        protected bool Burst { get; private set; }

        public int Charges
        {
            get => _charges;
            set
            {
                value = Mathf.Max(value, 0);
                if (_charges == value)
                    return;
                
                OnChargesChanged?.Invoke(value);
                _charges = value;
            }
        }
        
        protected Card CardPlace { get; private set; }

        public event Action<int> OnChargesChanged;

        public virtual void OnPlay(bool burst)
        {
            Burst = burst;
        }

        public virtual void OnPurge()
        {
            Charges = 0;
        }
        public virtual void OnDraw(){}

        public virtual void OnDiscard()
        {
            Charges = 0;
        }

        public virtual void OnUseAction()
        {
            ActionAvailbale = false;
            GameBoard.PlayerBoard.OnActionUsed?.Invoke(this);
        }

        public void ReadyAction() => ActionAvailbale = true;

        private void Awake()
        {
            GetComponent<ISpellAddedHandler>()?.OnSpellAdded(this);
        }

        protected async Task<List<T>> GetTargets<T>(LocalizedString message, int ammount, bool exactNumber) where T : MonoBehaviour
        {
            return await GameBoard.TargetChooser.StartTargetsChoose<T>(transform, message, ammount, exactNumber);
        }

        protected async Task<T> GetTarget<T>(LocalizedString message, bool compulsory)where T : MonoBehaviour
        {
            List<T> targets = await GetTargets<T>(message, 1, compulsory);
            return targets.Count > 0 ? targets[0] : null;
        }

        protected void AddCardToDiscardPile(Type type)
        {
            
        }

        private void RestoreAction() => ActionAvailbale = true;

        private void OnDestroy()
        {
            GetComponent<ISpellRemovedHandler>()?.OnSpellRemoved();
            if (HasAction)
            {
                GameBoard.Turn.OnPlayerTurnStart -= RestoreAction;
            }
        }

        public void OnCardPlaceChanged(Card newPlace)
        {
            CardPlace = newPlace;
        }
    }

    public enum SpellType
    {
        Nature,
        Sun,
        Moon,
        Blood,
        Action,
        None,
    }
}