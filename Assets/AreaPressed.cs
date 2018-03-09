using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets
{
    public class AreaPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action Action;
        private bool _ispressed = false;

        void Update()
        {
            if (_ispressed)
            {
                Action();
            }             
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _ispressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _ispressed = false;
        }

    }
}