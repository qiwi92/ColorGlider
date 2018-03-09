using DG.Tweening;
using UnityEngine;


namespace Assets
{
    public class Glider : MonoBehaviour
    {
        public GameObject GameObject;
        [Range(0.1f,1)] public float CollisionDistance;
        [Range(1, 10)] public float Height;
        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
        public SpriteRenderer SpriteRenderer;
        public ParticleSystem EngineParticleSystem;
        public ParticleSystem EngineDustParticleSystem;

        public void ResetPositionSmooth()
        {
            this.transform.DOMove(Vector3.down * Height, 0.7f);
        }

        public void ResetPosition()
        {
            this.transform.position = Vector3.down * Height;
        }

        public void SetColor(Color color)
        {
            SpriteRenderer.color = color;
            EngineParticleSystem.startColor = color;
            EngineDustParticleSystem.startColor = color;
        }

        public void Move(float speed, float maxWidth, Direction direction)
        {
            Direction curentDirection;

            if (this.transform.position.x > 0)
            {
                curentDirection = Direction.Right;
            }
            else
            {
                curentDirection = Direction.Left;
            }

            if (direction != curentDirection)
            {
                transform.position += speed * Vector3.right * ((float)direction) * Time.deltaTime;
            }
            else if (Mathf.Abs(this.transform.position.x) < maxWidth)
            {
                transform.position += speed * Vector3.right * ((float)direction) * Time.deltaTime;
            }                     
        }           
    }
}