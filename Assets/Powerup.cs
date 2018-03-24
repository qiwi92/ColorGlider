using UnityEngine;

namespace Assets
{
    public class PowerUp : MonoBehaviour, ICollider
    {
        private bool _isAlive;

        public float GetSize()
        {
            return 0.305f;
        }

        public ObjectType GetType()
        {
            return ObjectType.PowerUp;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

        public void SetState(bool isAlive)
        {
            _isAlive = isAlive;
        }

        public bool GetState()
        {
            return _isAlive;
        }
    }
}