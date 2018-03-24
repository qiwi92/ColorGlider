using UnityEngine;

namespace Assets
{
    public class PowerupsView : MonoBehaviour {

        public PowerUp DiamondPrefab;

        public int Amount;
        private float _speed;


        public ParticleSystem ParticleSystem;

        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public PowerUp[] PowerUps;
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        public float Height;


        public void SetUp(float width)
        {
            _width = width;
            PowerUps = new PowerUp[Amount];
            SetSpeed(0);

            for (var index = 0; index < PowerUps.Length; index++)
            {
                var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);
                var newPowerUp = Instantiate(DiamondPrefab, randomPos, Quaternion.identity);

                newPowerUp.IsAlive = true;
                PowerUps[index] = newPowerUp;

            }


            var mainModule = ParticleSystem.main;
            mainModule.duration = 4;
            mainModule.startColor = ColorPalette.PowerUp;
            mainModule.startDelay = 2f;

            _emitParams = new ParticleSystem.EmitParams();
            _emitParams.applyShapeToPosition = true;

        }

        public void Move()
        {
            foreach (var powerUp in PowerUps)
            {
                if (powerUp.transform.position.y < -Height)
                {
                    Reset(powerUp);
                }

                if (!powerUp.IsAlive)
                {
                    _emitParams.position = powerUp.transform.position;
                    ParticleSystem.Emit(_emitParams, 4);

                    Reset(powerUp);

                    powerUp.IsAlive = true;
                }
                else
                {
                    powerUp.transform.position += Vector3.down * _speed * Time.deltaTime;
                }

                
            }
        }

        public float SetSpeed(int score)
        {
            var baseSpeed = 3 + 0.1f * score;
            return _speed = Random.Range(baseSpeed, baseSpeed * 1.3f);
        }

        private void Reset(PowerUp diamond)
        {
            diamond.transform.position = new Vector3(Random.Range(-_width, _width), Height, 0);
        }

        public void KillAll()
        {
            foreach (var powerUp in PowerUps)
            {
                powerUp.IsAlive = false;
            }
        }
    }
}
