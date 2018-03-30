using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupView : MonoBehaviour, ICollider
    {
        [HideInInspector] public bool IsAlive;
        [HideInInspector] public bool CanSpawn;
        private float _counter = 0;
        private float _spawnChance = 0;

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
            _spawnChance = chance;
        }

        private void Update()
        {
            _counter += Time.deltaTime;
            if (_counter > 1 && !CanSpawn)
            {
                var chance = Random.Range(0, 1.0f);

                if (chance < _spawnChance)
                {
                    CanSpawn = true;
                }
                
                _counter = 0;
            }
        }
    }
}