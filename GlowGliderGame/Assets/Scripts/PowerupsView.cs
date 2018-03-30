using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupsView : MonoBehaviour {

        public PowerupView PowerupPrefab;
        public PowerupView ShieldPowerUp;
    
        public int Amount;
        public int AmountShields;
        private float _speed;


        public ParticleSystem ParticleSystem;

        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public PowerupView[] Powerups;
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        public float Height;


        public void SetUp(float width)
        {
            _width = width;
            Powerups = new PowerupView[Amount + AmountShields];
            SetSpeed(0);

            for (var index = 0; index < Amount; index++)
            {
                var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);
                var newPowerUp = Instantiate(PowerupPrefab, randomPos, Quaternion.identity);
                
                newPowerUp.SetColors(ColorPalette.Powerup[0]);
                newPowerUp.IsAlive = true;
                Powerups[index] = newPowerUp;
            }

            for (var index = Amount; index < Amount + AmountShields; index++)
            {
                var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);
                var newPowerUp = Instantiate(ShieldPowerUp, randomPos, Quaternion.identity);

                newPowerUp.SetColors(ColorPalette.Powerup[1]);
                newPowerUp.IsAlive = true;
                Powerups[index] = newPowerUp;
            }


            var mainModule = ParticleSystem.main;
            mainModule.duration = 4;
            
            mainModule.startDelay = 2f;

            _emitParams = new ParticleSystem.EmitParams {applyShapeToPosition = true};

        }

        public void Move()
        {
            foreach (var powerUp in Powerups)
            {
                if (powerUp.transform.position.y < -Height)
                {
                    Reset(powerUp);
                }

                if (!powerUp.IsAlive)
                {
                    _emitParams.position = powerUp.transform.position;

                    var mainModule = ParticleSystem.main;
                    mainModule.startColor = powerUp.GetColor();

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

        private void Reset(PowerupView diamond)
        {
            diamond.transform.position = new Vector3(Random.Range(-_width, _width), Height, 0);
        }

        public void KillAll()
        {
            foreach (var powerUp in Powerups)
            {
                powerUp.IsAlive = false;
            }
        }
    }
}
