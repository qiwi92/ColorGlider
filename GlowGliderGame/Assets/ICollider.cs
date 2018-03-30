using UnityEngine;

namespace Assets
{
    public interface ICollider
    {
        float GetSize();
        ObjectType GetObjectType();
        Vector3 GetPosition();
    }
}