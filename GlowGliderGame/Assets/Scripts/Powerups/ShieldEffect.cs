using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Powerups
{
    public class ShieldEffect : MonoBehaviour , IPowerupEffect
    {
        private float _counter = 0;
        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;

        private PowerupState _shieldState = PowerupState.Deactivated;
      
        public float Duration;

        public void Activate(Color color)
        {
            _shieldSpriteRenderer.color = color;
            _shieldSpriteRenderer.DOFade(1, 0.4f).SetEase(Ease.OutElastic);
            _counter = 0;
            _shieldState = PowerupState.Activated;
        }

        private void Update()
        {
            switch (_shieldState)
            {
                case PowerupState.Deactivated:
                    break;

                case PowerupState.Activated:
                    HandleActivated();
                    break;

                case PowerupState.Deactivation:
                    break;
            }
        }

        private void HandleActivated()
        {
            _counter += Time.deltaTime;
            
            if (_counter > Duration)
            {
                _shieldState = PowerupState.Deactivation;
                _shieldSpriteRenderer.DOFade(0, 0.2f).SetEase(Ease.Linear)
                    .SetLoops(10, LoopType.Yoyo)
                    .OnComplete(() =>
                    {
                        Deactivate();
                    });
            }
        }

        public bool IsActive()
        {
            return _shieldState == PowerupState.Activated || _shieldState == PowerupState.Deactivation;
        }

        public void Deactivate()
        {
            _shieldSpriteRenderer.DOFade(0, 0.3f);
            _shieldState = PowerupState.Deactivated;
        }

        public PowerupType GetPowerupType()
        {
            return PowerupType.Shield;
        }
    }
}