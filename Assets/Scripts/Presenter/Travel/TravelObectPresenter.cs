using Model.Travel;
using UnityEngine;
using View.Travel;

namespace Presenter.Travel
{
    public class TravelObectPresenter : MonoBehaviour
    {
        [SerializeField] private TravelObject _model;
        [SerializeField] private TravelObjectView _view;

        private void Awake()
        {
            _model.OnShow += _view.Show;
            _model.OnHide += _view.Hide;
        }
    }
}