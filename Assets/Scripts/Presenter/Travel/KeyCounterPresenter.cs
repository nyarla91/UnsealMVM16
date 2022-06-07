using System;
using Model.Global.Save;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presenter.Travel
{
    public class KeyCounterPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        [Inject] private PermanentSave _permanentSave;
        
        private void Start()
        {
            if (_permanentSave.Data.Keys <= 0)
                Destroy(gameObject);
            _text.text = _permanentSave.Data.Keys.ToString();
        }
    }
}