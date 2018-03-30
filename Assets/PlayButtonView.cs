using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class PlayButtonView : MonoBehaviour
    {
        public Button Button;
        private bool _isPlaying;

        public void Start()
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
            //if (IsPlaying)
            //{
            //    IsPlaying = false;
            //    return true;
            //}

            return _isPlaying;
        }

        private void SetStateToPlaying()
        {
            _isPlaying = true;
        }

        public void SetStateToNotPlaying()
        {
            _isPlaying = false;
        }
    }
}
