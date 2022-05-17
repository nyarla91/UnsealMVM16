﻿using System.Collections.Generic;
using Model.Combat.Cards;
using NyarlaEssentials;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public abstract class CardArea<T> : Transformer where T : Card
    {

        [SerializeField] private Transform _cardStandart;
        
        public Transform CardStandart => _cardStandart;
        protected List<T> Cards { get; } = new List<T>();
        public int Size => Cards.Count;
        public bool IsFull => Size == MaxSize;
        public abstract int MaxSize { get; }
        
        public void AddCard(T cardToAdd)
        {
            Cards.Add(cardToAdd);
            cardToAdd.transform.parent = transform;
            RearrangeCards();
        }

        public void RemoveCard(T cardToRemove)
        {
            Cards.Remove(cardToRemove);
            cardToRemove.transform.parent = null;
            RearrangeCards();
        }

        protected abstract void RearrangeCards();
    }
}