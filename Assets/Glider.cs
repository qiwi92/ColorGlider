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

       


        private Vector3 _mousePosition;
        public float moveSpeed = 0.01f;

        public void SetUp(Color color)
        {
            IsAlive = true;
        }

        private void Update()
        {
            //if (Input.GetMouseButton(0))
            //{
            //    _mousePosition = Input.mousePosition;
            //    _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            //    transform.position = Vector2.Lerp(Vector3.right* transform.position.x + Vector3.down*Height, Vector3.right * _mousePosition.x, moveSpeed);
            //}

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