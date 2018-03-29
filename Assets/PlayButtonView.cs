using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class PlayButtonView : MonoBehaviour
    {
        public Button Button;
        private bool _isPlaying;

        private void Start ()
        {
            _isPlaying = false;
            Button.onClick.AddListener(ClickAnimation);
            Button.onClick.AddListener(SetStateToPlaying);
        }

        public void ClickAnimation()
        {
            Button.transform.DOPunchScale(Vector3.one *0.3f, 0.2f);
        
        }

        public bool GetState()
        {
            if (_isPlaying)
            {
                _isPlaying = false;
                return true;
            }

            return false;
        }

        private void SetStateToPlaying()
        {
            _isPlaying = true;
        }
    }
}
