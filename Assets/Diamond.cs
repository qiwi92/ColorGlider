using UnityEngine;

namespace Assets
{
    public class Diamond : MonoBehaviour, ICollider
    {
        [HideInInspector] public int Value = 1;
        [HideInInspector] public bool IsAlive;
        [HideInInspector] public bool CanSapwn;

        public float GetSize()
        {
            return 0.305f;
        }

        public ObjectType GetObjectType()
        {
            return ObjectType.Diamond;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }
}