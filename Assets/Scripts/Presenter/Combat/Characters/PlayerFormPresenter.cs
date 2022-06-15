using DG.Tweening;
using Essentials;
using Model.Combat.GameAreas;
using Model.Combat.Shapeshifting;
using UnityEngine;
using Zenject;

namespace Presenter.Combat.Characters
{
    public class PlayerFormPresenter : Transformer
    {
        [SerializeField] private MeshRenderer _miniatureIcon;
        [SerializeField] private Transform _miniature;
        
        [Inject] private GameBoard _gameBoard;

        private void Awake()
        {
            _gameBoard.Turn.OnFormChanged += UpdateForm;
        }

        private void UpdateForm(Form form)
        {
            _miniatureIcon.material = form.Icon;
            _miniature.DOComplete();
            _miniature.DOLocalJump(_miniature.localPosition, 2, 1, 0.5f);
            _miniature.DOBlendablePunchRotation(new Vector3(0, 360, 0), 0.5f);
        }
    }
}