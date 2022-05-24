using System;
using Essentials;
using Essentials.Pointers;
using Model.Combat.Targeting;
using UnityEngine;

namespace Presenter.Combat.Targeting
{
    public class TargetChooserPresenter : MonoBehaviour
    {
        [SerializeField] private TargetChooser _targetChooser;
        [SerializeField] private LineRenderer _line;
        
        private Transform _origin;
        private Vector3 _lineCenter;
        
        private void Start()
        {
            _targetChooser.OnChooseStart += Show;
            _targetChooser.OnChooseEnd += Hide;
            _line.positionCount = 12;
            Vector2 screenPoint = new Vector2(Screen.width, Screen.height) * 0.5f;
            _lineCenter = RaycastMouseRegion(CameraProperties.Instance.Main.ScreenPointToRay(screenPoint));
        }

        private void Show(Transform origin) => _origin = origin;

        private void Hide() => _origin = null;

        private void FixedUpdate()
        {
            if (_origin == null)
            {
                _line.enabled = false;
                return;
            }
            _line.enabled = true;
            Vector3[] keyPoints = new Vector3[3];

            Vector2 originPoint = CameraProperties.Instance.Main.WorldToScreenPoint(_origin.position);
            keyPoints[0] = RaycastMouseRegion(CameraProperties.Instance.Main.ScreenPointToRay(originPoint));
            
            
            Vector2 mousePoint = PointerControls.Actions.Mouse.Position.ReadValue<Vector2>();
            keyPoints[2] = RaycastMouseRegion(CameraProperties.Instance.Main.ScreenPointToRay(mousePoint));

            Vector2 centerPoint = Vector2.Lerp(originPoint, mousePoint, 0.5f) + new Vector2(0, Screen.height * 0.5f);
            keyPoints[1] = RaycastMouseRegion(CameraProperties.Instance.Main.ScreenPointToRay(centerPoint));
            
            _line.SetPositions(BezierCurve.EvaluatePath(keyPoints, 12));
        }

        private Vector3 RaycastMouseRegion(Ray ray)
        {
            LayerMask mask = LayerMask.GetMask("UI");
            return Physics.Raycast(ray, out RaycastHit raycast, 5000, mask) ? raycast.point : Vector3.zero;
        }
    }
}