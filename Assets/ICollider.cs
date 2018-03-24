using UnityEngine;

namespace Assets
{
    public interface ICollider
    {
        float GetSize();
        ObjectType GetType();
        Vector3 GetPosition();
    }
}