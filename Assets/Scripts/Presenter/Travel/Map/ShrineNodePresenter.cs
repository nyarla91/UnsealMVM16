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

        [Inject] private SceneLoader _sceneLoader;

        private void Awake()
        {
            _model.OnPlayerEntered += () => _view.Show(this);
            _model.OnPlayerLeft += _view.Hide;
        }

        public void EditDeck()
        {
            _sceneLoader.LoadDeckbuilding();
        }
    }
}