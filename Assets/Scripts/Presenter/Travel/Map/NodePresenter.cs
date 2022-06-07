using System;
using Model.Travel.Map;
using UnityEngine;

namespace Presenter.Travel.Map
{
    public class NodePresenter : MonoBehaviour
    {
        [SerializeField] private Node _model;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _activeMaterial;
        [SerializeField] private Material _inactiveMaterial;

        private void Awake()
        {
            _model.OnSwitchInteractionActive += UpdateMaterial;
        }

        private void UpdateMaterial(bool active)
        {
            _meshRenderer.material = active ? _activeMaterial : _inactiveMaterial;
        }
    }
}