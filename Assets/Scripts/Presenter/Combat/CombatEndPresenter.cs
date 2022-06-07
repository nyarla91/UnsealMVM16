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
        [SerializeField] private AbilitiyTooltip _abilitiyTooltip;
        [SerializeField] private UIDialog _rewardScreen;
        [SerializeField] private RectTransform _rewardPosition;
        [SerializeField] private UIDialog _deathScreen;
        
        [Inject] private PermanentSave _permanentSave;
        [Inject] private GlobalTravelState _travelState;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private GameBoard _gameBoard;

        public AbilitiyTooltip AbilitiyTooltip => _abilitiyTooltip;

        public void ClaimReward()
        {
            _permanentSave.Data.CombatsCleared.Add(_travelState.NextCombatData.Name);
            _travelState.NextCombatData.Reward.ClaimReward(_permanentSave);
            _permanentSave.Save();
            _sceneLoader.LoadTravel();
        }

        public void ReviveAtShrine()
        {
            _travelState.Reset();
            _sceneLoader.LoadTravel();
        }
        
        private void Awake()
        {
            _gameBoard.EnemyPool.OnAllEnemiesDefeated += CallRewardScreen;
            _gameBoard.Player.OnDeath += CallDeathScreen;
        }

        private void CallRewardScreen()
        {
            _rewardScreen.FadeIn();
            _travelState.NextCombatData.Reward.ShowExample(_rewardPosition, this);
        }

        private void CallDeathScreen()
        {
            _deathScreen.FadeIn();
        }
    }
}