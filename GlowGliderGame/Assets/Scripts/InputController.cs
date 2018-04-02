using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class InputController : MonoBehaviour
    {
        //TODO parameters to config, remove Mono
        [Range(1, 2)] public float Sensibility;
        private readonly float _borderThreshold = 0.01f;


        public void Move(Transform trans,float offset,float maxWidth,float speed)
        {
            if (Input.GetMouseButton(0))
            {
                var mousePosition = GetMousePosition();
                var targetPos = ConvertMousePosIntoTargetPos(mousePosition,offset, Sensibility, maxWidth);

                trans.position = SmothOverMoveTo(trans.position, targetPos, speed);
            }
        }

        private Vector3 ConvertMousePosIntoTargetPos(Vector3 mousePos, float offset, float sensibility,float maxWidth)
        {
            var xOffset = mousePos.x* sensibility;

            if (xOffset > maxWidth)
            {
                return new Vector3(maxWidth, -offset, 0);
            }

            if (xOffset < -maxWidth)
            {
                return new Vector3(-maxWidth, -offset, 0);
            }

            return new Vector3(mousePos.x* sensibility, -offset, 0);
        }

        private Vector3 GetMousePosition()
        {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            return Camera.main.ScreenToWorldPoint(v3);
        }

        private Vector3 SmothOverMoveTo(Vector3 from, Vector3 to, float speed)
        {
            var distance = Vector3.Distance(from, to);

            var actualSpeed =  distance * speed;
            if (actualSpeed > speed)
            {
                actualSpeed = speed;
            }
            var step = actualSpeed * Time.smoothDeltaTime ;

            return Vector3.MoveTowards(from, to, step); 
        }

     
    }
}