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

        public ParticleSystem SwitchParticleSystem;
        public ParticleSystem SwitchParticleSystemBlured;

        private Vector3 _mousePosition;
        public float moveSpeed = 0.01f;

        public void SetUp(Color color)
        {
            IsAlive = true;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _mousePosition = Input.mousePosition;
                _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
                transform.position = Vector2.Lerp(Vector3.right* transform.position.x + Vector3.down*Height, Vector3.right * _mousePosition.x, moveSpeed);
            }

            if (this.IsAlive == false)
            {
                this.transform.position = Vector3.down * Height;
                IsAlive = true;
            }
        }

        public void SetColor(Color color)
        {
            SpriteRenderer.color = color;
            EngineParticleSystem.startColor = color;
            EngineDustParticleSystem.startColor = color;

            SwitchParticleSystem.startColor = color;
            SwitchParticleSystemBlured.startColor = color;
        }     
    }
}