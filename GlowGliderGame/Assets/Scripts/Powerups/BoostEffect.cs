using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Powerups
{
    public class BoostEffect : MonoBehaviour, IPowerupEffect
    {
        [SerializeField] private ParticleSystem BoostParticles;
        [SerializeField] private ParticleSystem BoostDebreeParticles;
        [SerializeField] private ParticleSystem SpeedEffectParticleSystem;
        [SerializeField] private float Duration;
        private float _counter = 0;

        private List<ParticleSystem> _particleSystems;

        private PowerupState _boostState;

        private void Start()
        {
            _boostState = PowerupState.Deactivated;
            _particleSystems = new List<ParticleSystem>
            {
                BoostParticles,
                BoostDebreeParticles,
                SpeedEffectParticleSystem
            };

            Debug.Log("Length: " + _particleSystems.Count);
        }

        private void Update()
        {
            switch (_boostState)
            {
                case PowerupState.Activation:
                    HandleActivation();
                    break;
                case PowerupState.Activated:
                    HandleActivated();
                    break;
                case PowerupState.Deactivation:
                    HandleDeactivation();
                    break;
                case PowerupState.Deactivated:
                    HandleDeactivated();
                    break;
            }
        }

        private void HandleActivation()
        {
            _counter = 0;
            ActivateParticles();
            _boostState = PowerupState.Activated;
        }

        private void HandleActivated()
        {
            _counter += Time.deltaTime;

            if (_counter > Duration)
            {
                _boostState = PowerupState.Deactivation;
            }
        }

        private void HandleDeactivation()
        {
            DeactivateParticles();
            _boostState = PowerupState.Deactivated;
        }

        private void HandleDeactivated()
        {

        }

        private void SetParticlesColor(Color color)
        {
            foreach (var particleSystems in _particleSystems)
            {
                var main = particleSystems.main;
                main.startColor = color;
            }
        }

        private void ActivateParticles()
        {
            foreach (var particleSystems in _particleSystems)
            {
                Debug.Log("Playing");
                particleSystems.Play();
            }
        }

        private void DeactivateParticles()
        {
            foreach (var particleSystems in _particleSystems)
            {
                particleSystems.Stop();
            }
        }

        public void Activate(Color color)
        {
            SetParticlesColor(color);
            _boostState = PowerupState.Activation;
        }

        public void Deactivate()
        {
            _boostState = PowerupState.Deactivation;
        }

        public bool IsActive()
        {
            return _boostState == PowerupState.Activated || _boostState == PowerupState.Deactivation;
        }

        public PowerupType GetPowerupType()
        {
            return PowerupType.Boost;
        }
    }
}