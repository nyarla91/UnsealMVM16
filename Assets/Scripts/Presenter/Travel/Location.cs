using Presenter.Travel.Camera;
using UnityEngine;

namespace Presenter.Travel
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private Locations _locations;
        [SerializeField] private int _index;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TravelCamera irrelevant))
            {
                _locations.EnableLight(_index, false);
            }   
        }
    }
}