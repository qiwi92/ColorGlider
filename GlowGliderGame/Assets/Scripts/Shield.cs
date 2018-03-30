using UnityEngine;

namespace Assets.Scripts
{
    public class Shield : MonoBehaviour , IPowerup
    {
        private bool _isActive;
        private float _counter;

        public float Duration;

        public Shield()
        {
            _isActive = false;
            _counter = 0;
        }

        public void Activate()
        {
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive)
            {
                _counter += Time.deltaTime;
                if (_counter > Duration)
                {
                    ResetShield();
                }
            }
        }

        private void ResetShield()
        {
            _isActive = false;
            _counter = 0;
        }

        public bool IsActive()
        {
            return _isActive;
        }
    }
}