using System.Collections.Generic;
using UnityEngine;

namespace LonelySoccer
{
    public class ReflectionHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _DummyLayer;
        [SerializeField] private int _MaxReflectionCount = 10;
        [SerializeField] private float _MaxDistance = 20f;

        public struct ReflectionResult
        {
            public bool IsGoal;
            public List<Vector3> WayPoints;
        }

        public ReflectionResult GetReflectionResult(Vector3 position, Vector3 startDirection)
        {
            var reflections = new ReflectionResult
            {
                WayPoints = new List<Vector3> {position}
            };

            var direction = startDirection;
            for (var i = 0; i < _MaxReflectionCount; i++)
            {
                var ray = new Ray(position, direction);
                if (Physics.Raycast(ray, out var hit, _MaxDistance, _DummyLayer))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    Debug.Log(direction.magnitude);
                    position = hit.point;
                    reflections.WayPoints.Add(hit.point);
                    
                    var goal = hit.collider.GetComponent<Goal>();
                    
                    if (goal)
                    { 
                        reflections.IsGoal = true;
                        break;
                    }
                }
                else
                {   
                    var endPosition = position + (direction.normalized * _MaxDistance);
                    reflections.WayPoints.Add((endPosition));
                    break;
                }
            }
            
            return reflections;
        }
    }
}
