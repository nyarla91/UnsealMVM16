using Model.Global;
using Model.Travel.Map;
using UnityEngine;
using View.Travel;
using Zenject;

namespace Presenter.Travel.Map
{
    public class ShrineNodePresenter : MonoBehaviour
    {
        [SerializeField] private ShrineNode _model;
        [SerializeField] private EditDeckButton _view;
        [SerializeField] private MeshRenderer _icon;
        [SerializeField] private Material _fastTravelEnabledMaterial;
        [SerializeField] private Material _fastTravelDisablesMaterial;

        [Inject] private SceneLoader _sceneLoader;

        private void Awake()
        {
            _model.OnPlayerEntered += () => _view.Show(this);
            _model.OnPlayerLeft += _view.Hide;
            _model.OnFastTravelSwitched += UpdateIcon;
        }

        private void UpdateIcon(bool fastTravelActive) => _icon.material =
            fastTravelActive ? _fastTravelEnabledMaterial : _fastTravelDisablesMaterial;

        public void EditDeck()
        {
            _sceneLoader.LoadDeckbuilding();
        }
    }
}