using UnityEngine;

namespace Assets
{
    public class PowerUp : MonoBehaviour, ICollider
    {
        [HideInInspector] public bool IsAlive;

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
    }
}