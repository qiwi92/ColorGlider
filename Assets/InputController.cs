﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class InputController : MonoBehaviour
    {
        public PlayButtonView PlayButton;

        //[HideInInspector] public float ScreenWidth;
        [HideInInspector] public Transform GliderTransform;


        private Direction _gliderMoveDirection;

        [Range(0, 1)] public float Tolerance;
        [Range(0, 1)] public float Sensibility;


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SetMoveDirection();
            }
            else
            {
                _gliderMoveDirection = Direction.None;
            }
        }

        private void SetMoveDirection()
        {
            var mousePosition = GetMousePosition();
            var targetPos = ConvertMousePosIntoTargetPos(mousePosition);
            var gliderPos = GliderTransform.position;

            var distance = Mathf.Abs(targetPos.x - gliderPos.x);
            var sign = (targetPos.x - gliderPos.x);

            if (sign < 0 && distance > Tolerance)
            {
                _gliderMoveDirection = Direction.Left;
            }
            else if (sign > 0 && distance > Tolerance)
            {
                _gliderMoveDirection = Direction.Right;
            }
            else if(distance < Tolerance)
            {
                _gliderMoveDirection = Direction.None;
            }
            else
            {
                throw new Exception("Case not covered");
            }
        }

        public Direction GetMoveDirection()
        {
            return _gliderMoveDirection;
        }

        private Vector3 ConvertMousePosIntoTargetPos(Vector3 mousePos)
        {
            var factor = 1 + Sensibility;
            return new Vector3(mousePos.x*factor,-2,0);
        }

        private Vector3 GetMousePosition()
        {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            return Camera.main.ScreenToWorldPoint(v3);
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawCube(_mousePosition, Vector3.one*0.3f);

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawCube(_targetPos, Vector3.one * 0.3f);
        //}
    }
}