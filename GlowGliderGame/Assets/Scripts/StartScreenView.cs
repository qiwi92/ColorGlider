using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StartScreenView : MonoBehaviour
    {
        public PlayButtonView PlayButton;
        public Button OpenShoptButton;
        public Button OpenScoreButton;
        public Text HighScoreText;
        public RectTransform StartScreenTransform;
        public Toggle ToggleSound;
        [SerializeField] private Image _toggledOnImage;
        [SerializeField] private Image _openScoreButtonImage;
        [SerializeField] private Image _openShopButtonImage;

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
            _toggledOnImage.color = color;
            _openScoreButtonImage.color = color;
            _openShopButtonImage.color = color;
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