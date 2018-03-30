using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StartScreenView : MonoBehaviour
    {
        public PlayButtonView PlayButton;
        public Button OpenShoptButton;
        public Text HighScoreText;
        public RectTransform StartScreenTransform;

        private float _initOffSet;

        private void Awake()
        {
            _initOffSet = StartScreenTransform.position.y;
        }

        public void SetHighScore(int score)
        {
            HighScoreText.text = "Your best: " + score.ToString("0");
        }

        public void SetColors(Color color)
        {
            PlayButton.ColoredCircleImage.color = color;
        }

        public void PlayAnimation()
        {
            var playing = PlayButton.GetState();
            if (playing)
            {
                StartScreenTransform.DOLocalMove(Vector3.up * StartScreenTransform.rect.height , 0.5f);
            }
            else
            {
                StartScreenTransform.DOLocalMove(Vector3.up * _initOffSet, 0.5f);
            }
            
        }
    }
}