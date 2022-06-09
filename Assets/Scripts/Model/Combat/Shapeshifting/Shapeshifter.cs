using System.Linq;
using Essentials;
using Model.Combat.GameAreas;
using Model.Global.Save;
using Presenter.Combat;
using UnityEngine;
using View.Cards;
using Zenject;

namespace Model.Combat.Shapeshifting
{
    public class Shapeshifter : ComponentInstantiator
    {
        [SerializeField] private AbilitiyTooltip _tooltip;
        [Inject] private GameBoard _gameBoard;
        [Inject] private PermanentSave _permanentSave;

        private void Start()
        {
            GenerateForms();
        }

        private void GenerateForms()
        {
            GameObject[] formPrefabs = _permanentSave.Data.FormsUnlcoked
                .Select(form => Resources.Load<GameObject>($"Forms/{form}")).ToArray();
            if (formPrefabs.Length <= 1)
                return;
            
            float leftMostIndex = (1 - formPrefabs.Length) * 0.5f;
            const float unitsPerIndex = 2;
            for (int i = 0; i < formPrefabs.Length; i++)
            {
                InstantiateForComponent(out Form form, formPrefabs[i].GetComponent<Form>(), transform);
                form.GameBoard = _gameBoard;
                form.GetComponent<FormPresenter>().Tooltip = _tooltip;
                form.transform.localPosition = new Vector3((leftMostIndex + i) * unitsPerIndex, 0, 0);
            }
        }
    }
}