using System.Linq;
using Essentials;
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
            Vector3[] keyPoints = { LerpEnds(0.1f), LerpEnds(0.5f) + Vector3.up * 1.2f, LerpEnds(0.9f)};
            Vector3[] line = BezierCurve.EvaluatePath(keyPoints, 6);
            _line.positionCount = 6;
            _line.SetPositions(line);

            Vector3 LerpEnds(float t) => Vector3.Lerp(ends[0].transform.position, ends[1].transform.position, t);
        }
    }
}