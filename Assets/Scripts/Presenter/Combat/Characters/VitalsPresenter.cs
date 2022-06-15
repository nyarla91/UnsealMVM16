using DG.Tweening;
using Essentials;
using Model.Combat.Characters;
using UnityEngine;
using View.Combat.Characters;
using View.Combat.PopUpNumber;
using Zenject;

namespace Presenter.Combat.Characters
{
    public class VitalsPresenter : DescendingObject
    {
        [SerializeField] private Character _character;
        [SerializeField] private Counter _healthCounter;
        [SerializeField] private Counter _armorCounter;
        [SerializeField] private PopUpNumber _damagePopUpPrefab;
        [SerializeField] private PopUpNumber _healPopUpPrefab;
        [SerializeField] private PopUpNumber _armorPopUpPrefab;
        [SerializeField] private Transform _miniature;

        private RectTransform _canvasTransform;

        
        private Canvas _canvas;
        [Inject] public Canvas Canvas
        {
            private get => _canvas;
            set
            {
                _canvas = value;
                _canvasTransform = _canvas.GetComponent<RectTransform>();
            }
        }

        private void Awake()
        {
            _character.OnHealthChanged += UpdateHealth;
            _character.OnArmorChanged += UpdateArmor;
            _character.OnTakeDamage += CreateDamagePopUp;
            _character.OnArmorAdded += CreateArmorPopUp;
            _character.OnRestoreHealth += CreateHealPopUp;
        }

        private void CreateHealPopUp(int value)
        {
            InstantiatePopUp(_healPopUpPrefab, value);
        }

        private void CreateArmorPopUp(int value)
        {
            InstantiatePopUp(_armorPopUpPrefab, value);
        }

        private void CreateDamagePopUp(int value)
        {
            _miniature.DOComplete();
            _miniature.DOLocalJump(_miniature.localPosition, 2, 1, 0.4f);
            _miniature.DOShakeRotation(0.4f, new Vector3(20, 0, 20));
            InstantiatePopUp(_damagePopUpPrefab, value);
        }

        private void UpdateArmor(int newArmor) => UpdateCounter(_armorCounter, newArmor);

        private void UpdateHealth(int newHealth) => UpdateCounter(_healthCounter, newHealth);

        private void UpdateCounter(Counter counter, int value)
        {
            if (value == 0)
                counter.Ascend(false);
            else
                counter.Descend();
            counter.Value = value;
        }

        private void InstantiatePopUp(PopUpNumber prefab, int value)
        {
            PopUpNumber number = Instantiate(prefab.gameObject, _canvasTransform).GetComponent<PopUpNumber>();
            number.RectTransform.anchoredPosition = CameraProperties.Instance.Main.WorldToScreenPoint(transform.position);
            number.Init(value);
        }
    }
}