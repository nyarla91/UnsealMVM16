using Essentials;
using Essentials.Pointers;
using Model.Cards;
using Model.Cards.Spells;
using UnityEngine;
using View.Cards;

namespace Presenter.Cards
{
    public class CardPresenter : MonoBehaviour, ISpellRemovedHandler, ISpellAddedHandler, ICardPlaceChangedHandler
    {
        [SerializeField] private CardView _view;
        [SerializeField] protected GameObject _playabelOutline;
        [SerializeField] private SerializedDictionary<string, Transform> _standarts;

        private Spell _spell;
        private PointerTarget _pointerTarget;
        private Card _card;

        public void Show() => _view.gameObject.SetActive(true);
        public void Hide() => _view.gameObject.SetActive(false);

        public void OnSpellAdded(Spell spell)
        {
            _spell = spell;
            _spell.OnChargesChanged += _view.UpdateCharges;
            _view.UpadteView(_spell);
        }

        public void OnCardPlaceChanged(Card newPlace)
        {
            _card = newPlace;
            _view.OffsetStandart = _standarts.Dictionary[newPlace.GetType().Name];
        }

        public void OnSpellRemoved()
        {
            if (_spell == null)
                return;

            _spell.OnChargesChanged -= _view.UpdateCharges;
            _spell = null;
        }

        private void Awake()
        {
            _pointerTarget = GetComponent<PointerTarget>();
            _pointerTarget.OnEnter += _view.Maximize;
            _pointerTarget.OnExit += _view.Minimize;
        }

        private void Update()
        {
            if (_card != null)
            {
                _playabelOutline.SetActive(_card.ShowPlayableOutline);
            }
        }
    }
}