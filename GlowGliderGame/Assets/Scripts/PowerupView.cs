using Assets.Scripts.Powerups;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupView : MonoBehaviour, ICollider
    {
        [HideInInspector] private bool _isAlive;
        [HideInInspector] public float Speed;
        private float _counter = 0;
        public float SpawnChance = 0;

        public PowerupItemState CurentPowerupItemState;

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

        public void SetSpawnChance(float chance)
        {
            SpawnChance = chance;
        }

        private void Update()
        {
            switch (CurentPowerupItemState)
            {
                case PowerupItemState.Spawn:
                    HandleSpawn();
                    break;
                case PowerupItemState.Alive:
                    HandleAlive();
                    break;
                case PowerupItemState.Dead:                 
                    HandleDead();
                    break;
                case PowerupItemState.Dying:
                    HandleDying();
                    break;
            }
        }

        private void HandleSpawn()
        {
            _isAlive = true;
            CurentPowerupItemState = PowerupItemState.Alive;
        }

        private void HandleAlive()
        {
            if (!_isAlive)
            {
                CurentPowerupItemState = PowerupItemState.Dying;
            }
        }

        private void HandleDying()
        {
            _isAlive = false;
            CurentPowerupItemState = PowerupItemState.Dead;
        }
        private void HandleDead()
        {
            if (CanSpawn())
            {
                CurentPowerupItemState = PowerupItemState.Spawn;
            }
        }

        private bool CanSpawn()
        {
            _counter += Time.deltaTime;
            if (_counter > 1)
            {
                var chance = Random.Range(0, 1.0f);

                if (chance < SpawnChance)
                {
                    return true;
                }

                _counter = 0;
            }

            return false;
        }   
    }
}