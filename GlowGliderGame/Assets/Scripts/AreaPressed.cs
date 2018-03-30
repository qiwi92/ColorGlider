using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class AreaPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action Action;
        private bool _isPressed = false;

        //void Update()
        //{
        //    if (_isPressed)
        //    {
        //        Action();
        //    }             
        //}
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }

        public bool IsPressed()
        {
            return _isPressed;
        }

    }
}