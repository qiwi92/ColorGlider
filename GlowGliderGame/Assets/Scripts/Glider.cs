﻿using Assets.Scripts.Money;
using Assets.Scripts.Powerups;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class Glider : MonoBehaviour
    {
        [HideInInspector] public int Score;

        private int _collectedCircles = 0;
        [HideInInspector] public int Index;

        [HideInInspector] public CollisionStates CollisionState;

        [HideInInspector] public DiamondsView DiamondsView;
        [HideInInspector] public CirclesView CirclesView;
        [HideInInspector] public PowerupsView PowerupsView;

        [Range(0.1f,1)] public float CollisionDistance;

        private readonly float _height = 2.80f;
        public float Height => _height;

        [SerializeField] private float _speed;
        public float Speed => _speed;

        [HideInInspector] public int Id;
        [HideInInspector] public bool IsAlive;
    
        public SpriteRenderer GliderSpriteFilled;
        public SpriteRenderer GliderSpriteOutline;
        public ParticleSystem EngineParticleSystem;
        public ParticleSystem EngineDustParticleSystem;


        [HideInInspector] public bool IsBoosted;

        public Collisions Collisions;

        [HideInInspector] public Sounds Sounds;

        [HideInInspector] public ColorPalette ColorPalette;
        

        public void Setup()
        {
            PowerupsView.ShieldEffect.Deactivate();
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


        public void HandleCollision(ICollider collijder)
        {
            switch (collijder.GetObjectType())
            {
                case ObjectType.Circle:
                    HandleCircleCollision(collijder as CircleView);
                    break;
                case ObjectType.PowerUp:
                    HandlePowerUpCollision(collijder as PowerupView);
                    break;
                case ObjectType.Diamond:
                    HandleDiamondCollision(collijder as DiamondView);
                    break;
            }
        }

        private void Update()
        {
            if (PowerupsView.BoostEffect.IsActive())
            {
                IsBoosted = true;
            }
            else
            {
                IsBoosted = false;
            }
        }

        private void HandleCircleCollision(CircleView cirle)
        {
            CollisionState = CollisionStates.JustCollided;

            if (PowerupsView.BoostEffect.IsActive())
            {
                Score += cirle.Value;
                cirle.Alive = false;
                return;
            }

            

            if (cirle.Id == Id)
            {
                Index = _collectedCircles % 3;

                SetColor(Index);
                Sounds.PlayCollectSfx(Index);

                _collectedCircles += 1;

                Score += cirle.Value;

                cirle.Alive = false;

                if (PowerupsView.ShieldEffect.IsActive())
                {
                    PowerupsView.ShieldEffect.ChangeColor(ColorPalette.Colors[Id]);
                }
            }
            else
            {
                if(SROptions.InvincibilityCheatActive)
                    return;

                
                if (PowerupsView.ShieldEffect.IsActive())
                {
                    PowerupsView.ShieldEffect.Deactivate();
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
            //PowerupsView.SetSpeed(Score);
            powerup.CurentPowerupItemState = PowerupItemState.Dying;

            var powerupType = powerup.PowerupType;

            switch (powerupType)
            {
                case PowerupType.Shield:
                    PowerupsView.ShieldEffect.Activate(ColorPalette.Colors[Id]);
                    break;
                case PowerupType.Boost:
                    PowerupsView.BoostEffect.Activate(ColorPalette.Colors[Id]);
                    break;
            }
        }
     
        private void HandleDiamondCollision(DiamondView diamond)
        {
            Sounds.PlayDiamondSfx();
            //DiamondsView.SetSpeed(Score);

            MoneyService.Instance.AddMoney(diamond.Value);
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

                EngineParticleSystem.startColor = ColorPalette.Colors[Id];
                EngineDustParticleSystem.startColor = ColorPalette.Colors[Id];
            }        
        }

        public float GetSpeed()
        {
            if (IsBoosted)
            {
                return _speed*0.15f;
            }

            return _speed;
        }
    }

    public enum CollisionStates
    {
        None,
        JustCollided,
    }
}