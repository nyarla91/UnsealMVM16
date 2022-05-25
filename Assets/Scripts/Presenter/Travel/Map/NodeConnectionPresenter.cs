using System;
using System.Linq;
using Model.Travel.Map;
using UnityEngine;

namespace Presenter.Travel.Map
{
    [ExecuteAlways]
    public class NodeConnectionPresenter : MonoBehaviour
    {
        [SerializeField] private NodeConnection _connection;
        [SerializeField] private LineRenderer _line;

        private void Awake()
        {
            _connection.OnEndsUpdated += UpdateLine;
        }

        private void UpdateLine(Node[] ends)
        {
            _line.positionCount = 2;
            _line.SetPositions(ends.Select(end => ((Component) end).transform.position).ToArray());
        }
    }
}