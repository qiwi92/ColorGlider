using UnityEngine;

namespace Assets.Scripts.Powerups
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
            newBoostPowerup.CurentPowerupItemState = PowerupItemState.Dead;


            randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);
            var newShieldPowerup = Instantiate(ShieldPowerUp, randomPos, Quaternion.identity);
            newShieldPowerup.PowerupType = PowerupType.Shield;
            newShieldPowerup.SetColors(ColorPalette.PowerupShield);
            newShieldPowerup.CurentPowerupItemState = PowerupItemState.Dead;

            Powerups[0] = newBoostPowerup;
            Powerups[1] = newShieldPowerup;

            var mainModule = ParticleSystem.main;
            mainModule.duration = 4;
            mainModule.startDelay = 2f;
            _emitParams = new ParticleSystem.EmitParams {applyShapeToPosition = true};


            foreach (var powerUp in Powerups)
            {
                powerUp.SetSpawnChance(0.2f);
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

                if (powerUp.CurentPowerupItemState == PowerupItemState.Alive)
                {
                    powerUp.transform.position += Vector3.down * _speed * Time.deltaTime;
                }

                if (powerUp.CurentPowerupItemState == PowerupItemState.Dying)
                {
                    _emitParams.position = powerUp.transform.position;

                    var mainModule = ParticleSystem.main;
                    mainModule.startColor = powerUp.GetColor();

                    ParticleSystem.Emit(_emitParams, 4);

                    Reset(powerUp);
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
                powerUp.CurentPowerupItemState = PowerupItemState.Dying;
            }
        }
    }
}
