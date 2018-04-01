using UnityEngine;

namespace Assets.Scripts.Powerups
{
    public class PowerupsView : MonoBehaviour {

        public PowerupView PowerupPrefab;
        public PowerupView ShieldPowerUp;
    
        public ParticleSystem ParticleSystem;

        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public PowerupView[] Powerups;
        [HideInInspector] public ColorPalette ColorPalette;

        private IPowerupData _shieldData;
        private IPowerupData _boostData;

        private float _width;
        public float Height;

        private bool _isInitialized = false;
        private float _shieldCounter;
        private int _shieldLevel;

        private float _boostCounter;
        private int _boostLevel;
        private readonly SpeedData _speedData = new SpeedData();
        
        public ShieldEffect ShieldEffect;
        public BoostEffect BoostEffect;

        public void SetUp(float width)
        {
            _width = width;
            Powerups = new PowerupView[2];

            var randomPos = new Vector3(Random.Range(-_width, _width), Height, 0);

            _boostData = new BoostData(); 
            var newBoostPowerup = Instantiate(PowerupPrefab, randomPos, Quaternion.identity);
            newBoostPowerup.PowerupType = PowerupType.Boost;
            newBoostPowerup.SetColors(ColorPalette.PowerupBoost);
            newBoostPowerup.CurentPowerupItemState = PowerupItemState.Dead;


            _shieldData = new ShieldData();
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
        }

        public void SetStartValues()
        {
            _boostLevel = PlayerPrefs.GetInt(PowerupType.Boost.ToString());
            BoostEffect.Duration = _boostData.GetActiveDuration(_boostLevel);
            Powerups[0].SpawnChance = _boostData.GetSpawnChance(_boostLevel, -1);
            _boostCounter = 0;

            _shieldLevel = PlayerPrefs.GetInt(PowerupType.Shield.ToString());
            ShieldEffect.Duration = _shieldData.GetActiveDuration(_boostLevel);
            Powerups[1].SpawnChance = _shieldData.GetSpawnChance(_shieldLevel,-1);
            _shieldCounter = 0;

            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized) return;

            _boostCounter += Time.deltaTime;
            Powerups[0].SpawnChance = _boostData.GetSpawnChance(_boostLevel, _boostCounter);

            _shieldCounter += Time.deltaTime;
            Powerups[1].SpawnChance = _shieldData.GetSpawnChance(_shieldLevel, _shieldCounter);
        }

        public void Move()
        {
            var smoothDeltaTime = Time.smoothDeltaTime;

            foreach (var powerUp in Powerups)
            {
                if (powerUp.transform.position.y < -Height)
                {
                    Reset(powerUp);
                }

                if (powerUp.CurentPowerupItemState == PowerupItemState.Alive)
                {
                    powerUp.transform.position += Vector3.down * powerUp.Speed * smoothDeltaTime;
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



        public void SetSpeed(int score, bool isBoosted)
        {
            foreach (var powerup in Powerups)
            {
                powerup.Speed = _speedData.GetSpeed(score, isBoosted);
            }
        }

        private void Reset(PowerupView powerup)
        {
            powerup.CurentPowerupItemState = PowerupItemState.Dying;
            powerup.transform.position = new Vector3(Random.Range(-_width, _width), Height, 0);
        }

        public void KillAll()
        {
            foreach (var powerup in Powerups)
            {
                Reset(powerup);
                powerup.CurentPowerupItemState = PowerupItemState.Dying;
            }
        }
    }
}
