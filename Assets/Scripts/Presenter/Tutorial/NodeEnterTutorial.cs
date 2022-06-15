using System;
using Model.Travel.Map;
using UnityEngine;

namespace Presenter.Tutorial
{
    public class NodeEnterTutorial : TutorialScreen
    {
        [SerializeField] private Node _node;

        private void Awake()
        {
            _node.OnPlayerEnterHere += Show;
        }
    }
}