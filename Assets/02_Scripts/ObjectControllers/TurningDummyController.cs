using UnityEngine;

namespace LonelySoccer.ObjectControllers
{
    public class TurningDummyController : MonoBehaviour
    {
        private void OnMouseDown()
        {
            transform.RotateAround(transform.position, Vector3.up, 30);
        }
    }
}
