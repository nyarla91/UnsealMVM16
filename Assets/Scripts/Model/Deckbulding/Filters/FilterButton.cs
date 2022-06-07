using System;
using Essentials.Pointers;
using Model.Cards.Spells;
using UnityEngine;
using Zenject;
using PointerType = Essentials.Pointers.PointerType;

namespace Model.Deckbulding.Filters
{
    public abstract class FilterButton : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        
        [Inject] private DeckbuildingBoard _deckbuildingBoard;
        protected abstract Func<Spell, bool> _criteria { get; }

        private bool _active;

        public event Action<bool> OnSwitch;
        
        private void Awake()
        {
            _pointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            if (button != PointerType.Left)
                return;

            _active = !_active;
            if (_active)
                _deckbuildingBoard.Libary.FilterCriterias.Add(_criteria);
            else
                _deckbuildingBoard.Libary.FilterCriterias.Remove(_criteria);
            OnSwitch?.Invoke(_active);
            _deckbuildingBoard.Libary.FilterAndRearrange();
        }
    }
}