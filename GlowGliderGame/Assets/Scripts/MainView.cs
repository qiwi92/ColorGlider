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
        public StartScreenView StartScreenView;
        public PowerupShopView ShopView;
        public HighScorePanelView HighScoreView;

        public GameObject ShopPanel;
        public GameObject ScorePanel;

        private Button _openShopButton;
        private Button _closeShopButton;

        private Button _openScoreButton;
        private Button _closeScoreButton;

        private bool _shopIsOpen;
        private bool _scoreIsOpen;

        private float _screenWidth;
        

        public void Setup( int highscore)
        {
            _shopIsOpen = false;
            _scoreIsOpen = false;

            _screenWidth = 2*Camera.main.orthographicSize * Camera.main.aspect;

            MoveShopPanel();
            MoveScorePanel();

            StartScreenView.SetHighScore(highscore);
            HighScoreView.Initialize(new HighScoreModel());

            var shopItemModels = new List<PowerupItemShopModel>
            {
                new PowerupItemShopModel(PowerupType.Shield, new ShieldData()),
                new PowerupItemShopModel(PowerupType.Boost, new BoostData()),
            };

            ShopView.CreatePowerupItemShopViews(shopItemModels);


            _openShopButton = StartScreenView.OpenShoptButton;
            _openShopButton.onClick.AddListener(MoveShopPanel);
            _closeShopButton = ShopView.CloseShoptButton;
            _closeShopButton.onClick.AddListener(MoveShopPanel);

            _openScoreButton = StartScreenView.OpenScoreButton;
            _openScoreButton.onClick.AddListener(MoveScorePanel);
            _closeScoreButton = HighScoreView.PlayButton;
            _closeScoreButton.onClick.AddListener(MoveScorePanel);
        }

        private void MoveScorePanel()
        {
            if (_scoreIsOpen)
            {
                HighScoreView.PanelTransform.DOMove(Vector3.zero, 0.4f);
                _scoreIsOpen = false;
            }
            else
            {
                HighScoreView.PanelTransform.DOMove(Vector3.left * _screenWidth, 0.4f);
                _scoreIsOpen = true;
            }
        }


        private void MoveShopPanel()
        {
            if (_shopIsOpen)
            {
                ShopView.PowerUpShopCanvasTransform.DOMove(Vector3.zero,0.4f);
                _shopIsOpen = false;
            }
            else
            {
                ShopView.PowerUpShopCanvasTransform.DOMove(Vector3.right * _screenWidth, 0.4f);
                _shopIsOpen = true;
            }
        }

        public void DeactivateShopCanvas()
        {
            ShopPanel.SetActive(false);
        }

        public void ActivateShopCanvas()
        {
            ShopPanel.SetActive(true);
        }

        public void DeactivateScoreCanvas()
        {
            ScorePanel.SetActive(false);
        }

        public void ActivateScoreCanvas()
        {
            ScorePanel.SetActive(true);
        }

        public void SetColors(Color color)
        {
            StartScreenView.SetColors(color);
            ShopView.SetColors(color);
            HighScoreView.SetColors(color);
        }
    }
}