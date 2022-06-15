using Model.Combat;
using Model.Combat.Characters;
using Model.Combat.GameAreas;
using Model.Global;
using Model.Global.Save;
using UnityEngine;
using View;
using View.Cards;
using Zenject;

namespace Presenter.Combat
{
    public class CombatEndPresenter : MonoBehaviour
    {
        [SerializeField] private CombatEnd _model;
        [SerializeField] private AbilitiyTooltip _abilitiyTooltip;
        [SerializeField] private RectTransform _rewardPosition;
        [SerializeField] private UIDialog _rewardScreen;
        [SerializeField] private UIDialog _deathScreen;
        
        [Inject] private GlobalTravelState _travelState;
        [Inject] private Pause _pause;
        [Inject] private GameBoard _gameBoard;

        public AbilitiyTooltip AbilitiyTooltip => _abilitiyTooltip;

        public void OnClaimPressed() => _model.ExitVictory();
        public void OnRevivePressed() => _model.ExitDeath();
        
        private void Awake()
        {
            _gameBoard.EnemyPool.OnAllEnemiesDefeated += CallRewardScreen;
            _gameBoard.Player.OnDeath += CallDeathScreen;
        }

        private void CallRewardScreen()
        {
            _pause.Begin(_model);
            _rewardScreen.FadeIn();
            _travelState.NextCombatData.Reward.ShowExample(_rewardPosition, this);
        }

        private void CallDeathScreen()
        {
            _pause.Begin(_model);
            _deathScreen.FadeIn();
        }
    }
}