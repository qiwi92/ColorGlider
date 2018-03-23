﻿using DG.Tweening;
using UnityEngine;


namespace Assets
{
    public partial class Glider : MonoBehaviour
    {
        public GameObject GameObject;
        [Range(0.1f,1)] public float CollisionDistance;
        private readonly float _height = 2.80f;
        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
        [HideInInspector] public bool HasHitBox;
        public HitBoxState CurrentHitBoxState;
    
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

        public void SetColorOutline(Color color)
        {
            GliderSpriteOutline.DOColor(color, 0.4f);
        }


        //public void SetGliderStates(bool input)
        //{
        //    switch (CurrentHitBoxState)
        //    {
        //        case Glider.HitBoxState.BecommingUntargetable:
        //            HandleBecommingUntargetable();
        //            break;
        //        case Glider.HitBoxState.Untargetable:
        //            HandleUntargetable(input);
        //            break;
        //        case Glider.HitBoxState.BecommingTargetable:
        //            HandleBecommingTargetable();
        //            break;
        //        case Glider.HitBoxState.Targetable:
        //            HandleTargetable(input);
        //            break;
        //    }
        //}

        private void HandleBecommingUntargetable()
        {
            GliderSpriteFilled.DOFade(0, 0.2f);

            EngineParticleSystem.Stop();
            EngineDustParticleSystem.Stop();


            CurrentHitBoxState = HitBoxState.Untargetable;
        }

        private void HandleUntargetable(bool input)
        {
            HasHitBox = false;

            if (!input)
            {
                CurrentHitBoxState = HitBoxState.BecommingTargetable;
            }
        }

        private void HandleBecommingTargetable()
        {
            EngineParticleSystem.Play();
            EngineDustParticleSystem.Play();

            CurrentHitBoxState = HitBoxState.Targetable;
            
        }

        private void HandleTargetable(bool input)
        {
            HasHitBox = true;

            if (input)
            {
                CurrentHitBoxState = HitBoxState.BecommingUntargetable;
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