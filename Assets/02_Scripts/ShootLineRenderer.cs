using System.Collections.Generic;
using UnityEngine;

namespace LonelySoccer
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShootLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _LineRenderer;


        public void RenderLineWithPoints(List<Vector3> wayPoints)
        {
            _LineRenderer.positionCount = wayPoints.Count + 1;
            _LineRenderer.SetPosition(0, wayPoints[0]);

            for (var index = 0; index < wayPoints.Count; index++)
            {
                var wayPoint = wayPoints[index];
                _LineRenderer.SetPosition(index + 1, wayPoint);
            }
        }
    
        public void RenderLineWithOutPoints(List<Vector3> wayPoints)
        {
            _LineRenderer.positionCount = 2;
        
            _LineRenderer.SetPosition(0, wayPoints[0]);
            _LineRenderer.SetPosition(1, wayPoints[1]);
        }

        public void SetIsVisible(bool isVisible)
        {
            _LineRenderer.enabled = isVisible;
        }
    }
}
