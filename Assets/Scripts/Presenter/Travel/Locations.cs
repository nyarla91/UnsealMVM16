using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Presenter.Travel
{
    public class Locations : MonoBehaviour
    {
        [SerializeField] private List<Transform> _lights;

        public void EnableLight(int index, bool instantly)
        {
            for (int i = 0; i < _lights.Count; i++)
            {
                _lights[i].DOKill();
                _lights[i].DOLocalMoveY(i == index ? 50 : 120, instantly ? 0 : 1);
            }
        }
    }
}