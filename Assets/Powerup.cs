using UnityEngine;

namespace Assets
{
    public class Powerup : MonoBehaviour, ICollider
    {
        [HideInInspector] public bool IsAlive;

        public float GetSize()
        {
            return 0.305f;
        }

        public ObjectType GetObjectType()
        {
            return ObjectType.PowerUp;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }
}