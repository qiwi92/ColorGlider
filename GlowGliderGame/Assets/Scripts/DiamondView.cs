using UnityEngine;

namespace Assets.Scripts
{
    public class DiamondView : MonoBehaviour, ICollider
    {
        [HideInInspector] public int Value = 1;
        [HideInInspector] public bool IsAlive;
        [HideInInspector] public bool CanSapwn;
        [HideInInspector] public float Speed;

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