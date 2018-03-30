using UnityEngine;

namespace Assets.Scripts
{
    public interface ICollider
    {
        float GetSize();
        ObjectType GetObjectType();
        Vector3 GetPosition();
    }
}