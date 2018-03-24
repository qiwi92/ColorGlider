using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class InputController : MonoBehaviour
    {
        public RectTransform LeftAreaTransform;
        public RectTransform RightAreaTransform;

        public RectTransform MainCanvasTransform;

        public Image RightAreaImage;
        public Image LeftAreaImage;

        public AreaPressed LeftAreaPressed;
        public AreaPressed RighAreaPressed;

        public Image UnlockProgressImageRight;
        public Image UnlockProgressImageLeft;
        public Image UnlockProgressFill;
        public Image UnlockProgressFillTriangle;
        public Image RightTutorialArrow;
        public Image LeftTutorialArrow;
        public Text TutorialText;

        public Text GameStateText;
        public Text HighScore;

        private float _currentPressedTime;
        private Color _color;

        public bool TwoFingersConfirmation()
        {
            var canvasWidth = MainCanvasTransform.sizeDelta.x;
            var counter = 0;

            if (LeftAreaPressed.IsPressed() || Input.GetKey(KeyCode.LeftArrow))
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth * 3.0f / 4.0f, 0.4f);
                UnlockProgressImageLeft.DOFillAmount(0.5f, 0.5f).SetEase(Ease.OutExpo);
                LeftTutorialArrow.DOFade(0, 0.4f).SetEase(Ease.OutExpo);
                counter++;
            }

            else
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth / 4, 0.4f);
                UnlockProgressImageLeft.DOFillAmount(0, 0.5f).SetEase(Ease.OutExpo);
                LeftTutorialArrow.DOFade(1, 0.4f).SetEase(Ease.OutExpo);
            }

            if (RighAreaPressed.IsPressed() || Input.GetKey(KeyCode.RightArrow))
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
                UnlockProgressImageRight.DOFillAmount(0.5f, 0.5f).SetEase(Ease.OutExpo);
                RightTutorialArrow.DOFade(0, 0.4f).SetEase(Ease.OutExpo);
                counter++;
            }
            else
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth / 4, 0.4f);
                UnlockProgressImageRight.DOFillAmount(0, 0.5f).SetEase(Ease.OutExpo);
                RightTutorialArrow.DOFade(1, 0.4f).SetEase(Ease.OutExpo);
            }

            if (counter == 0)
            {
                GameStateText.text = "Game Over";
                HighScore.DOFade(1, 0.4f);
                UnlockProgressFill.DOFade(0, 0.4f);
                UnlockProgressFillTriangle.DOFade(0, 0.4f);
                TutorialText.DOFade(1, 0.2f);

            }
            else if (counter == 1)
            {
                GameStateText.text = "New Game";
                HighScore.DOFade(0, 0.4f);
                UnlockProgressFill.DOColor(Color.white, 0.4f);
                UnlockProgressFillTriangle.DOFade(1, 0.4f);
                TutorialText.DOFade(1, 0.2f);
            }
            else if (counter == 2)
            {
                GameStateText.text = "Start";
                HighScore.DOFade(0, 0.4f);
                UnlockProgressFill.DOColor(_color, 0.4f);
                UnlockProgressFillTriangle.DOFade(1, 0.4f);
                TutorialText.DOFade(0, 0.2f);

                _currentPressedTime += Time.deltaTime;
            }

            if (counter != 2)
            {
                _currentPressedTime = 0f;
            }

            if (_currentPressedTime > 0.35f)
            {
                _currentPressedTime = 0f;
                return true;
            }

            return false;
        }

        public void MoveStartPanels()
        {
            var canvasWidth = MainCanvasTransform.sizeDelta.x;
            LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth * 3.0f / 4.0f, 0.4f);
            RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
        }

        public void SetColors(Color color)
        {
            _color = color;
            RightAreaImage.DOColor(color, 0.4f);
            LeftAreaImage.DOColor(color, 0.4f);
        }
    }
}