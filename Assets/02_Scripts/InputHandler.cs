using System;
using UnityEngine;

namespace LonelySoccer
{
    public class InputHandler : MonoBehaviour
    {
        private bool _wasDown;
        private Vector3 _lastMousePosition;

        public delegate void DragEvent(Vector3 deltaInput);

        public event DragEvent DidDrag;
        public event Action DragEnd;
        public event Action DragStart;
    
    
        void Update()
        {
            var isDown = Input.GetMouseButton(0);
            var mousePos = Input.mousePosition;

            // Drag start
            if (isDown && !_wasDown)
            {
                DragStart?.Invoke();

            }

            // Drag continue
            if (isDown && _wasDown)
            {
                var deltaPos = mousePos - _lastMousePosition;
                DidDrag?.Invoke(deltaPos);
            }

            // Drag end
            if (!isDown && _wasDown)
            {
                DragEnd?.Invoke();
            }

            _lastMousePosition = mousePos;
            _wasDown = isDown;
        }
    }
}
