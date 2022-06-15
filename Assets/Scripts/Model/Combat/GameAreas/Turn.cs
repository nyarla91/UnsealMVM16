using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Essentials;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Combat.Effects.Inner;
using Model.Combat.Shapeshifting;
using Model.Global;
using Model.Localization;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public class Turn : Transformer
    {
        [SerializeField] private GameBoard _gameBoard;

        private bool _firstTurn = true;

        private Form _currentForm;
        private readonly LocalizedString _chooseFormMessage = new LocalizedString
        (
            "Choose new form",
            "Выберите новый облик"
        );
        private readonly LocalizedString _chooseCardsToPurge = new LocalizedString
        (
            "Choose 3 cards to purge",
            "Выберите и очистите 3 карты"
        );
        private readonly LocalizedString _chooseCardsToPurgeBuffed = new LocalizedString
        (
            "Choose 4 cards to purge",
            "Выберите и очистите 4 карты"
        );
        private readonly LocalizedString _discardAnyCardsMessage = new LocalizedString
        (
            "Discard any cards",
            "Сбросьте любые карты"
        );

        public bool FormDifferenceBuff { get; set; }
        
        public bool IsPlayerTurn { get; private set; } = true;
        public int CardsPlayedThisTurn { get; private set; }
        public bool EndTurnButtonPressed { get; set; }

        public event Action OnPlayerTurnStart;
        public event Action OnNotFirstTurnStart;
        public event Action OnPlayerTurnEnd;
        public event Action OnEnemyTurnStart;
        public event Action<Form> OnFormChosen;
        public event Action<Form> OnFormChanged;
        public event Action OnFormNotChanged;

        public void AddCardPlayed()
        {
            CardsPlayedThisTurn++;
        }

        private async void EndTurn()
        {
            if (_gameBoard.EffectQueue.EffectInProgress || _gameBoard.TargetChooser.ChooseActive || !IsPlayerTurn)
                return;
            
            OnPlayerTurnEnd?.Invoke();
            await WaitForEffectsAndChoose();
            
            IsPlayerTurn = false;
            List<CardOnBoard> cardsToPurge = await _gameBoard.TargetChooser.StartTargetsChoose<CardOnBoard>
                (_gameBoard.PlayerDiscardPile.transform, FormDifferenceBuff ? _chooseCardsToPurgeBuffed : _chooseCardsToPurge, FormDifferenceBuff ? 4 : 3, true);
            foreach (var card in cardsToPurge)
            {
                _gameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, card));
            }
            await WaitForEffectsAndChoose();
            
            List<CardInHand> cardsToDiscard = await _gameBoard.TargetChooser.StartTargetsChoose<CardInHand>
                (_gameBoard.PlayerDiscardPile.transform, _discardAnyCardsMessage, 15, false);
            foreach (var card in cardsToDiscard)
            {
                _gameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.1f, card));
            }
            
            await WaitForEffectsAndChoose();

            OnEnemyTurnStart?.Invoke();

            _gameBoard.EffectQueue.AddEffect(new StartTurnEffect(this, 0.5f));
        }

        private void DrawCardsUpToMax()
        {
            int cardsToDraw = Mathf.Max(5 - _gameBoard.PlayerHand.Size, 0);
            _gameBoard.PlayerDeck.DrawCards(cardsToDraw);
        }

        [DontCallFromSpells]
        public async void StartTurn()
        {
            if (IsPlayerTurn)
                return;
            
            CardsPlayedThisTurn = 0;
            DrawCardsUpToMax();
            await WaitForEffectsAndChoose();
            
            await ChooseForm();
            
            IsPlayerTurn = true;
            OnPlayerTurnStart?.Invoke();
            if (_firstTurn)
            {
                OnNotFirstTurnStart?.Invoke();
                _firstTurn = false;
            }
        }

        private async Task ChooseForm()
        {
            await _gameBoard.EffectQueue.WaitForEffects();
            await Task.Delay(1);
            Form newForm = await _gameBoard.TargetChooser.StartTargetChoose<Form>
                (_gameBoard.Player.transform, _chooseFormMessage, true);
            
            OnFormChosen?.Invoke(newForm);
            FormDifferenceBuff =  _gameBoard.PlayerBoard.FormDifferenceBuff = _currentForm != null && !_currentForm.Equals(newForm);
            if (FormDifferenceBuff)
            {
                OnFormChanged?.Invoke(newForm);
            }
            else if (_currentForm != null)
            {
                OnFormNotChanged?.Invoke();
            }
            _currentForm?.Exit();
            newForm?.Enter();
            _currentForm = newForm;
        }

        public async Task WaitForEffectsAndChoose()
        {
            while (_gameBoard.EffectQueue.EffectInProgress || _gameBoard.TargetChooser.ChooseActive)
            {
                await Task.Delay(50);
            }
        }

        private void Update()
        {
            if (EndTurnButtonPressed)
            {
                EndTurn();
            }
            EndTurnButtonPressed = false;

            if (Input.GetKeyDown(KeyCode.X))
            {
                GameObject.FindObjectOfType<SceneLoader>().LoadTravel();
            }
        }

        private void Start()
        {
            DrawCardsUpToMax();
            ChooseForm();
        }
    }
}