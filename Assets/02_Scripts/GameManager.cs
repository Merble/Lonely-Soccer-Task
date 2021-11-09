using System.Collections.Generic;
using LonelySoccer.ObjectControllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace LonelySoccer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _MinAngle = 210;
        [SerializeField] private float _MaxAngle = 330;
        
        [SerializeField] private ShootLineRenderer _ShootLineRenderer;
        [SerializeField] private BallController _BallController;
        [SerializeField] private InputHandler _InputHandler;
        [SerializeField] private ReflectionHandler _ReflectionHandler;

        [FormerlySerializedAs("_inputToAngleMultiplier")] [SerializeField] private float _InputToAngleMultiplier = 1;
    
        private float _angle;
    
        [FormerlySerializedAs("_NoHitLineLengthOfLine")] [SerializeField] private float _NoHitLineLength;
    
        private void Awake()
        {
            _InputHandler.DragStart += InputHandlerOnDragStart;
            _InputHandler.DidDrag += InputHandlerOnDidDrag;
            _InputHandler.DragEnd += InputHandlerOnDragEnd;
        }

        private void InputHandlerOnDragStart()
        {
            _ShootLineRenderer.SetIsVisible(true);
        }

        private void InputHandlerOnDidDrag(Vector3 deltaInput)
        {
            _angle += deltaInput.x * _InputToAngleMultiplier;
            _angle = Mathf.Clamp(_angle, _MinAngle, _MaxAngle); // Making sure that angle has bounds.
        
            var reflectionResult = GetReflectionResultForAngle(out var ballPosition, out var startDirection);
        
            DrawShootLine(reflectionResult.WayPoints, ballPosition, startDirection);   // Set line renderer with positions
        }

        private ReflectionHandler.ReflectionResult GetReflectionResultForAngle(out Vector3 ballPosition, out Vector3 startDirection)
        {
            ballPosition = _BallController.transform.position;
            startDirection =  GetShootDirectionForAngle();
        
            Debug.DrawRay(ballPosition, startDirection * 5f, Color.red);

            return _ReflectionHandler.GetReflectionResult(ballPosition, startDirection);   // Get reflected positions and isGoal variable
        }

        private Vector3 GetShootDirectionForAngle()
        {
            return Quaternion.AngleAxis(_angle, Vector3.up) * Vector3.right;
        }

        private void InputHandlerOnDragEnd()
        {
            _ShootLineRenderer.SetIsVisible(false);

            var reflectionResult = GetReflectionResultForAngle(out _, out _);
            
            if (reflectionResult.IsGoal)
            {
                MoveBall(reflectionResult.WayPoints);
            }
        }

        private void DrawShootLine(List<Vector3> wayPoints, Vector3 startPosition, Vector3 startDirection)
        {
            if (wayPoints.Count > 2)   // Hit something
            {
                _ShootLineRenderer.RenderLineWithPoints(wayPoints);
            }
            else   // Do not hit anything
            {
                if (wayPoints.Count == 0)
                {
                    wayPoints.Insert(0,startPosition);
                }
                else if (wayPoints.Count == 1)
                {
                    var endPosition = startPosition + startDirection.normalized * _NoHitLineLength;
                    wayPoints.Add(endPosition);
                }
                _ShootLineRenderer.RenderLineWithOutPoints(wayPoints);
            }
        }

        private void MoveBall(List<Vector3> wayPoints)
        {
            _BallController.Move(wayPoints);
        }
    }
}
