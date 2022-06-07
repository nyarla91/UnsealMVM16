using System;
using System.Collections.Generic;
using Essentials;
using Model.Cards;
using Presenter.Cards;
using UnityEngine;
using View.Cards;

namespace Model.Combat.GameAreas
{
    public abstract class CardArea<TCard> : Transformer where TCard : Card
    {
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private Transform _cardStandart;
        [SerializeField] private AbilitiyTooltip abilitiyTooltip;
        
        public Transform CardStandart => _cardStandart;
        protected List<TCard> Cards { get; set; } = new List<TCard>();
        public int Size => Cards.Count;
        public bool IsFull => Size == MaxSize;
        protected virtual bool RearrangeAutomatically => true;
        protected virtual int MaxSize => Int32.MaxValue;
        
        public void AddCard(TCard cardToAdd)
        {
            Cards.Add(cardToAdd);
            cardToAdd.transform.parent = transform;
            if (RearrangeAutomatically)
                RearrangeCards();
        }

        public void RemoveCard(TCard cardToRemove)
        {
            Cards.Remove(cardToRemove);
            cardToRemove.transform.parent = null;
            if (RearrangeAutomatically) 
                RearrangeCards();
        }

        protected TCard CreateCard(string spellName, Vector3 startingPosition)
        {
            Type spellType = Type.GetType(spellName);
            if (spellType == null)
            {
                throw new Exception($"There is no {spellName} spell");
            }
            return CreateCard(spellType, startingPosition);
        }

        protected TCard CreateCard(Type spellType, Vector3 startingPosition)
        {
            GameObject card = Instantiate(_cardPrefab, startingPosition, Quaternion.identity, transform);
            card.AddComponent(spellType);
            TCard cardInPlace = card.AddComponent<TCard>();
            PassBoard(ref cardInPlace);
            cardInPlace.GetComponent<CardPresenter>().Tooltip = abilitiyTooltip;
            cardInPlace.Init();
            return cardInPlace;
        }

        protected abstract void PassBoard(ref TCard cardInPlace);

        protected abstract void RearrangeCards();
    }
}