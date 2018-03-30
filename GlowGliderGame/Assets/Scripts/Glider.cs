using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class Glider : MonoBehaviour
    {
        [HideInInspector] public int Score;
        [HideInInspector] public int Money;

        private int _collectedCircles = 0;
        [HideInInspector] public int Index;

        [HideInInspector] public CollisionStates CollisionState;

        [HideInInspector] public DiamondsView DiamondsView;
        [HideInInspector] public CirclesView CirclesView;
        [HideInInspector] public PowerupsView PowerupsView;

        [Range(0.1f,1)] public float CollisionDistance;
        private readonly float _height = 2.80f;
        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
    
        public SpriteRenderer GliderSpriteFilled;
        public SpriteRenderer GliderSpriteOutline;
        public ParticleSystem EngineParticleSystem;
        public ParticleSystem EngineDustParticleSystem;

        public Shield Shield;



        public Collisions Collisions;

        [HideInInspector] public Sounds Sounds;

        [HideInInspector] public ColorPalette ColorPalette;

        public void Setup()
        {
            Shield.Deactivate();
            Collisions = new Collisions(this, CirclesView.Circles, DiamondsView.Diamonds, PowerupsView.Powerups);
        }

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
            switch (collider.GetObjectType())
            {
                case ObjectType.Circle:
                    HandleCircleCollision(collider as CircleView);
                    break;
                case ObjectType.PowerUp:
                    HandlePowerUpCollision(collider as PowerupView);
                    break;
                case ObjectType.Diamond:
                    HandleDiamondCollision(collider as DiamondView);
                    break;
            }
        }

        private void HandleCircleCollision(CircleView cirle)
        {
            CollisionState = CollisionStates.JustCollided;

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
                if(SROptions.InvincibilityCheatActive)
                    return;

                if (Shield.IsActive())
                {
                    Shield.Deactivate();
                    cirle.Alive = false;
                    return;
                }

                _collectedCircles = 0;
                IsAlive = false;
                CirclesView.KillAll();
                DiamondsView.KillAll();
                PowerupsView.KillAll();
            }
        }

        private void HandlePowerUpCollision(PowerupView powerup)
        {
            Sounds.PlayPowerUpSfx();
            PowerupsView.SetSpeed(Score);
            powerup.IsAlive = false;

            var powerupType = powerup.PowerupType;

            switch (powerupType)
            {
                case PowerupType.Shield:
                    Shield.Activate();
                    break;
                case PowerupType.Boost:

                    break;
            }


        }
     
        private void HandleDiamondCollision(DiamondView diamond)
        {
            Sounds.PlayDiamondSfx();
            DiamondsView.SetSpeed(Score);

            Money += diamond.Value;

            DiamondsView.SetNewValue(Money);
            diamond.IsAlive = false;
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

                var colorWithoutAlpha = ColorPalette.Colors[Id];
                colorWithoutAlpha.a = 0;
                Shield.ShieldSpriteRenderer.DOColor(colorWithoutAlpha, 0.4f);

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