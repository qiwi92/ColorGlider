using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupView : MonoBehaviour, ICollider
    {
        [HideInInspector] public bool IsAlive;

        public SpriteRenderer OutlineSpriteRenderer;
        public SpriteRenderer LogoSpriteRenderer;

        [HideInInspector] public PowerupType PowerupType; 

        private Color _color;

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

        public void SetColors(Color color)
        {
            _color = color;
            OutlineSpriteRenderer.color = color;
            LogoSpriteRenderer.color = color;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}