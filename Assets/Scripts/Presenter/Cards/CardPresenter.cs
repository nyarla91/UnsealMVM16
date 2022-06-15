using System.Collections;
using Essentials;
using Essentials.Pointers;
using Model.Cards;
using Model.Cards.Spells;
using UnityEngine;
using View.Cards;
using Zenject;

namespace Presenter.Cards
{
    public class CardPresenter : MonoBehaviour, ISpellRemovedHandler, ISpellAddedHandler, ICardPlaceChangedHandler
    {
        [SerializeField] private CardView _view;
        [SerializeField] protected GameObject _playabelOutline;
        [SerializeField] private SerializedDictionary<string, Transform> _standarts;

        private Spell _spell;
        private PointerTarget _pointerTarget;
        private Card _model;
        private Coroutine _tooltipCoroutine;
        
        [field: Inject] public AbilitiyTooltip Tooltip { get; set; }

        public void Show()
        {
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        public void OnSpellAdded(Spell spell)
        {
            _spell = spell;
            _spell.OnChargesChanged += _view.UpdateCharges;
            _view.UpadteView(_spell);
        }

        public void OnCardPlaceChanged(Card newPlace)
        {
            _model = newPlace;
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
            _pointerTarget.OnEnter += MaximizeView;
            _pointerTarget.OnExit += MinimizeView;
        }

        private void MinimizeView()
        {
            _view.Minimize();
            if (this != null)
                _tooltipCoroutine?.Stop(this);
            _tooltipCoroutine = null;
            Tooltip.Hide();
        }

        private void MaximizeView()
        {
            if (!_model.InteractableOnPause && _model.Pause.IsPaused)
                return;
            _view.Maximize();
            _tooltipCoroutine = StartCoroutine(TooltipDelay());
        }

        private IEnumerator TooltipDelay()
        {
            yield return new WaitForSeconds(1);
            Tooltip.Show(Tooltip.DescriptionToExlanation(_spell.Description.Eng));
        }

        private void Update()
        {
            if (_model != null)
            {
                _playabelOutline.SetActive(_model.ShowPlayableOutline);
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}