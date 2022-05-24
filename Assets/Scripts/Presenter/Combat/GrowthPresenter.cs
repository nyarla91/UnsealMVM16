using Model.Combat.GameAreas;
using UnityEngine;
using View.Combat.Characters;
using Zenject;

namespace Presenter.Combat
{
    public class GrowthPresenter : MonoBehaviour
    {
        [SerializeField] private Token _token;
        
        [Inject]
        private GameBoard _board;

        private void Awake()
        {
            _board.PlayerBoard.OnGrowthChanged += UpdateGrowth;
        }

        private void UpdateGrowth(int growth)
        {
            _token.Value = growth;
            _token.Flip();
            if (growth == 0)
                _token.Ascend(false);
            else
                _token.Descend();
        }
    }
}