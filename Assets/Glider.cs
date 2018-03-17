using DG.Tweening;
using UnityEngine;


namespace Assets
{
    public class Glider : MonoBehaviour
    {
        public GameObject GameObject;
        [Range(0.1f,1)] public float CollisionDistance;
        private readonly float _height = 2.80f;
        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
        [HideInInspector] public float Energy = 100;
        public bool HasHitBox;
        public HitBoxState HitBox;
        public SpriteRenderer GliderSpriteFilled;
        public SpriteRenderer GliderSpriteOutline;
        public ParticleSystem EngineParticleSystem;
        public ParticleSystem EngineDustParticleSystem;

        public void ResetPositionSmooth()
        {
            this.transform.DOMove(Vector3.down * _height, 0.7f);
        }

        public void ResetPosition()
        {
            this.transform.position = Vector3.down * _height;
        }

        public void SetColor(Color color)
        {
            GliderSpriteFilled.DOColor(color, 0.4f);
            GliderSpriteOutline.DOColor(color, 0.4f);

            EngineParticleSystem.startColor = color;
            EngineDustParticleSystem.startColor = color;
        }

        public enum HitBoxState
        {
            BecommingUntargetable,
            Untargetable,
            BecommingTargetable,
            Targetable,
        }

        public void HandleBecommingUntargetable()
        {
            HasHitBox = false;

            GliderSpriteFilled.DOFade(0, 0.2f);
            EngineParticleSystem.Stop();
            EngineDustParticleSystem.Stop();
            

            HitBox = HitBoxState.Untargetable;
        }

        public void HandleUntargetable()
        {
            if (Energy > 0)
            {
                Energy -= 1;
            }

            if (HasHitBox || Energy < 0.5f)
            {
                HandleBecommingTargetable();
            }
        }

        public void HandleBecommingTargetable()
        {
            HasHitBox = true;

            GliderSpriteFilled.DOFade(1, 0.2f);
            EngineParticleSystem.Play();
            EngineDustParticleSystem.Play();

            HitBox = HitBoxState.Targetable;
        }

        public void HandleTargetable()
        {
            if (Energy < 100)
            {
                Energy += 0.2f;
            }

            if (!HasHitBox)
            {
                HandleBecommingUntargetable();
            }
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