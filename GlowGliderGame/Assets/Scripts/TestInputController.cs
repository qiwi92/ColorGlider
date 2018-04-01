using UnityEngine;

namespace Assets.Scripts
{
    public class TestInputController : MonoBehaviour
    {
        public float Height;
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = SmothOverMoveTo(Vector3.zero, transform.position, GetMousePosition(),  6, 1.3f);
            }
        }

        private Vector3 SmothOverMoveTo(Vector3 anker, Vector3 from, Vector3 to, float speed, float ratio)
        {
            var targetPos = Vector3.LerpUnclamped(anker, to, ratio);
            var distance = Vector3.Distance(from, targetPos);

            float step = distance * Time.smoothDeltaTime * speed;

            return Vector3.MoveTowards(from, targetPos, step);
        }

        private Vector3 GetMousePosition()
        {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            return Camera.main.ScreenToWorldPoint(v3);
        }
    }
}