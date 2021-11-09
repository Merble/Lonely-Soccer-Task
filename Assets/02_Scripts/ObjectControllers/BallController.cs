using System.Collections.Generic;
using UnityEngine;

namespace LonelySoccer.ObjectControllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private float _BallSpeed = 5f;
    
        // Waypoints is the array of all points to move including end point.
        public void Move(List<Vector3> wayPoints)
        {
            // TODO: Calculate speed based on waypoint distance
        
            var _travelTime = 0f;
            var distance = 0f;
            var delay = 0f;
            var oldWayPoint = gameObject.transform.position;
        
            foreach (var wayPoint in wayPoints)
            {
                distance = (wayPoint - oldWayPoint).magnitude;
                _travelTime = distance / _BallSpeed; //  t = V / X
            
                LeanTween.move(gameObject, wayPoint, _travelTime).setDelay(delay);
                delay += _travelTime;

                oldWayPoint = wayPoint;
            }
        }
    }
}