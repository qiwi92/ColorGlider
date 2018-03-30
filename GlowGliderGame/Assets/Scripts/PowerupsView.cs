using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupsView : MonoBehaviour {

        public PowerupView PowerupPrefab;
        public PowerupView ShieldPowerUp;
    
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
            Powerups = new PowerupView[2];
            SetSpeed(0);

            var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);

            var newBoostPowerup = Instantiate(PowerupPrefab, randomPos, Quaternion.identity);
            newBoostPowerup.PowerupType = PowerupType.Boost;
            newBoostPowerup.SetColors(ColorPalette.PowerupBoost);
            newBoostPowerup.IsAlive = true;
            
            
            var newShieldPowerup = Instantiate(ShieldPowerUp, randomPos, Quaternion.identity);
            newShieldPowerup.PowerupType = PowerupType.Shield;
            newShieldPowerup.SetColors(ColorPalette.PowerupShield);
            newShieldPowerup.IsAlive = true;

            Powerups[0] = newBoostPowerup;
            Powerups[1] = newShieldPowerup;

            var mainModule = ParticleSystem.main;
            mainModule.duration = 4;
            mainModule.startDelay = 2f;
            _emitParams = new ParticleSystem.EmitParams {applyShapeToPosition = true};


            foreach (var powerUp in Powerups)
            {
                powerUp.SetSpawnChance(0.5f);
            }
        }

        public void Move()
        {
            foreach (var powerUp in Powerups)
            {
                if (powerUp.transform.position.y < -Height)
                {
                    Reset(powerUp);
                }

                if (!powerUp.IsAlive && powerUp.CanSpawn)
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
