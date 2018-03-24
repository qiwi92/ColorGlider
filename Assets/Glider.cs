using DG.Tweening;
using UnityEngine;


namespace Assets
{
    public class Glider : MonoBehaviour
    {
        public int Score;
        public int Money;

        private int _collectedCircles = 0;
        public int Index;

        public CollisionStates CollisionState;

        public GameObject GameObject;
        [Range(0.1f,1)] public float CollisionDistance;
        private readonly float _height = 2.80f;
        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
    
        public SpriteRenderer GliderSpriteFilled;
        public SpriteRenderer GliderSpriteOutline;
        public ParticleSystem EngineParticleSystem;
        public ParticleSystem EngineDustParticleSystem;

        public Sounds Sounds;

        public ColorPalette ColorPalette;

        public void ResetPositionSmooth()
        {
            this.transform.DOMove(Vector3.down * _height, 0.7f);
        }

        public void ResetPosition()
        {
            this.transform.position = Vector3.down * _height;
        }

        

        public void SetColorOutline(Color color)
        {
            GliderSpriteOutline.DOColor(color, 0.4f);
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

        public void HandleCollision(ICollider collider)
        {
            switch (collider.GetType())
            {
                case ObjectType.Circle:
                    HandleCircleCollision(collider as Circle);
                    break;
                case ObjectType.PowerUp:
                    HandlePowerUpCollision(collider as PowerUp);
                    break;
                case ObjectType.Diamond:
                    HandleDiamondCollision(collider as Diamond);
                    break;
            }
        }

        private void HandleCircleCollision(Circle cirle)
        {
            if (cirle.Id == Id)
            {
                Index = _collectedCircles % 3;

                SetColor(Index);
                Sounds.PlayCollectSfx(Index);

                _collectedCircles += 1;

                Score += cirle.Value;

                cirle.Alive = false;
            }
            else
            {
                _collectedCircles = 0;
                IsAlive = false;
            }
        }

        private void HandlePowerUpCollision(PowerUp powerUp)
        {
            
        }

        private void HandleDiamondCollision(Diamond diamond)
        {
            Money += diamond.Value;
        }



        public void SetColor(int index)
        {
            if (index == 2)
            {
                int previousId = Id;

                int randomId = Random.Range(0, 3);

                Id = randomId;

                if (previousId == randomId)
                {
                    Id = (previousId + 1) % 3;
                }

                GliderSpriteFilled.DOColor(ColorPalette.Colors[Id], 0.4f);
                GliderSpriteOutline.DOColor(ColorPalette.Colors[Id], 0.4f);

                EngineParticleSystem.startColor = ColorPalette.Colors[Id];
                EngineDustParticleSystem.startColor = ColorPalette.Colors[Id];
            }        
        }



    }

    public enum CollisionStates
    {
        None,
        JustCollided,
    }
}