using Model.Combat.GameAreas;
using UnityEngine;
using View.Combat.Characters;
using Zenject;

namespace Presenter.Combat.Characters
{
    public class BurstPresenter : MonoBehaviour
    {
        [SerializeField] private Token _token;
        
        [Inject]
        private GameBoard _board;

        private void Awake()
        {
            _board.PlayerBoard.OnBurstChanged += UpdateBurst;
        }

        private void UpdateBurst(int growth)
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