using Model.Combat.GameAreas;
using Zenject;

namespace Presenter.Tutorial
{
    public class BoardTutorial : TutorialScreen
    {
        [Inject] private GameBoard _gameBoard;
        
        private void Awake()
        {
            _gameBoard.Turn.OnNotFirstTurnStart += Show;
        }
    }
}