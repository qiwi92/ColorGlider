using System.Collections.Generic;
using Assets.Scripts.Powerups;
using DG.Tweening;
using Highscore;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] private Image _leftImage;
        [SerializeField] private Image _rightImage;

        [SerializeField]
        private StartScreenView _startScreenView;

        private PowerupShopView _shopView;
        private HighScorePanelView _highScoreView;

        private Button _openShopButton;
        private Button _closeShopButton;

        private Button _openScoreButton;
        private Button _closeScoreButton;

        private bool _shopIsOpen;
        private bool _scoreIsOpen;

        private float _screenWidth;

        public StartScreenView StartScreenView => _startScreenView; //todo these should not be directly accesible here
        public PowerupShopView PowerupShopView => _shopView;
        public HighScorePanelView HighScoreView => _highScoreView;


        public void Setup(HighScorePanelView highScorePanelView, PowerupShopView shopView)
        {
            _highScoreView = highScorePanelView;
            _shopView = shopView;

            _shopIsOpen = false;
            _scoreIsOpen = false;

            _screenWidth = 2*Camera.main.orthographicSize * Camera.main.aspect;

            MoveShopPanel();
            MoveScorePanel();

            _highScoreView.Initialize(new HighScoreModel());

            _shopView.Setup();

            _openShopButton = _startScreenView.OpenShoptButton;
            _openShopButton.onClick.AddListener(MoveShopPanel);
            _closeShopButton = _shopView.CloseShoptButton;
            _closeShopButton.onClick.AddListener(MoveShopPanel);

            _openScoreButton = _startScreenView.OpenScoreButton;
            _openScoreButton.onClick.AddListener(MoveScorePanel);
            _closeScoreButton = _highScoreView.PlayButton;
            _closeScoreButton.onClick.AddListener(MoveScorePanel);
        }

        private void MoveScorePanel()
        {
            if (_scoreIsOpen)
            {
                _highScoreView.PanelTransform.DOMove(Vector3.zero, 0.4f);
                _scoreIsOpen = false;
            }
            else
            {
                _highScoreView.PanelTransform.DOMove(Vector3.left * _screenWidth, 0.4f);
                _scoreIsOpen = true;
            }
        }

        public void TryOpenScore(bool highScoreUnlocked)
        {
            if(highScoreUnlocked)
                MoveScorePanel();
        }

        private void MoveShopPanel()
        {
            if (_shopIsOpen)
            {
                _shopView.PowerUpShopCanvasTransform.DOMove(Vector3.zero,0.4f);
                _shopIsOpen = false;
            }
            else
            {
                _shopView.PowerUpShopCanvasTransform.DOMove(Vector3.right * _screenWidth, 0.4f);
                _shopIsOpen = true;
            }
        }

        public void DeactivateShopCanvas()
        {
            _shopView.Panel.SetActive(false);
        }

        public void ActivateShopCanvas()
        {
            _shopView.Panel.SetActive(true);
        }

        public void DeactivateScoreCanvas()
        {
            _highScoreView.Panel.SetActive(false);
        }

        public void ActivateScoreCanvas()
        {
            _highScoreView.Panel.SetActive(true);
        }

        public void PanelColorChange(Color color, int index)
        {
            if (index == 2)
            {
                var newColor = color;
                newColor.a = 0.3f;
                var newColorTarget = newColor;
                newColorTarget.a = 0.2f;
                _leftImage.DOColor(newColor, 0.2f).OnComplete(() => { _leftImage.DOColor(newColorTarget, 0.2f); });
                _rightImage.DOColor(newColor, 0.2f).OnComplete(() => { _rightImage.DOColor(newColorTarget, 0.2f); });
            }               
        }

        public void SetColors(Color color)
        {
            _startScreenView.SetColors(color);
            _shopView.SetColors(color);
            _highScoreView.SetColors(color);
        }

        public void SetHighScoreButtonState(bool isUnlocked)
        {
            _openScoreButton.gameObject.SetActive(isUnlocked);
        }

        public void SetShopButtonState(bool shopUnlocked)
        {
            _openShopButton.gameObject.SetActive(shopUnlocked);
        }
    }
}