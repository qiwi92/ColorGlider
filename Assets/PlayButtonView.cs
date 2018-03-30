using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class PlayButtonView : MonoBehaviour
    {
        public Button Button;
        public Image ColoredCircleImage;
        private bool _isPlaying;

        public void Start()
        {
            _isPlaying = false;

            Button.onClick.AddListener(SetStateToPlaying);
            Button.onClick.AddListener(ClickAnimation);
        }

        public void Update()
        {
            var colors = Button.colors;
            colors.normalColor = _isPlaying ? Color.green : Color.red;
            Button.colors = colors;
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
