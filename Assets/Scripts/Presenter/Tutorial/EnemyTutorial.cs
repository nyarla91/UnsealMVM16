using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Presenter.Tutorial
{
    public class EnemyTutorial : TutorialScreen
    {
        [Inject] private GameBoard _gameBoard;
        
        private void Awake()
        {
            _gameBoard.Turn.OnEnemyTurnStart += Show;
        }
    }
}