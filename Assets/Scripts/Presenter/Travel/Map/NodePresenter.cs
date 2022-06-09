using System.Collections;
using Essentials;
using Essentials.Pointers;
using Model.Localization;
using Model.Travel.Map;
using UnityEngine;
using View.Cards;
using Zenject;

namespace Presenter.Travel.Map
{
    public class NodePresenter : MonoBehaviour
    {
        [SerializeField] private PointerTarget _pointerTarget;
        [SerializeField] private Node _model;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _activeMaterial;
        [SerializeField] private Material _inactiveMaterial;
        [SerializeField] private LocalizedString _label;
        [SerializeField] private LocalizedString _description;

        [Inject] private AbilitiyTooltip _tooltip;

        private Coroutine _tooltipDelay;
        
        private void Awake()
        {
            _model.OnSwitchInteractionActive += UpdateMaterial;
            _pointerTarget.OnEnter += StartShowTooltipDelay;
            _pointerTarget.OnExit += HideTooltip;
        }

        private void StartShowTooltipDelay()
        {
            if (_tooltipDelay != null)
                return;
            _tooltipDelay = StartCoroutine(ShowTooltipDelay());
        }

        private IEnumerator ShowTooltipDelay()
        {
            yield return new WaitForSeconds(1);
            _tooltip.Show($"{_label.Localized}\n \n{_description.Localized}");
            _tooltipDelay = null;
        }

        private void HideTooltip()
        {
            _tooltip.Hide();
            _tooltipDelay?.Stop(this);
            _tooltipDelay = null;
        }

        private void UpdateMaterial(bool active)
        {
            _meshRenderer.material = active ? _activeMaterial : _inactiveMaterial;
        }
    }
}