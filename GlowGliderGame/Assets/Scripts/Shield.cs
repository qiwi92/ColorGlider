using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shield : MonoBehaviour , IPowerup
    {
        private float _counter = 0;
        public SpriteRenderer ShieldSpriteRenderer;

        private ShieldState _shieldState = ShieldState.Deactivated;
      
        public float Duration;

        public void Activate()
        {
            ShieldSpriteRenderer.DOFade(1, 0.4f).SetEase(Ease.OutElastic);
            _counter = 0;
            _shieldState = ShieldState.Activated;
        }

        private void Update()
        {
            switch (_shieldState)
            {
                case ShieldState.Deactivated:
                    break;

                case ShieldState.Activated:
                    HandleActivated();
                    break;

                case ShieldState.Deactivation:
                    break;
            }
        }

        private void HandleActivated()
        {
            _counter += Time.deltaTime;
            
            if (_counter > Duration)
            {
                _shieldState = ShieldState.Deactivation;
                ShieldSpriteRenderer.DOFade(0, 0.2f).SetEase(Ease.Linear)
                    .SetLoops(10, LoopType.Yoyo)
                    .OnComplete(() =>
                    {
                        Deactivate();
                    });
            }
        }

        private enum ShieldState
        {
            Deactivated,
            Activated,
            Deactivation
        }

        public bool IsActive()
        {
            return _shieldState == ShieldState.Activated || _shieldState == ShieldState.Deactivation;
        }

        public void Deactivate()
        {
            ShieldSpriteRenderer.DOFade(0, 0.3f);
            _shieldState = ShieldState.Deactivated;
        }

        public PowerupType GetPowerupType()
        {
            return PowerupType.Shield;
        }
    }
}