using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainView : MonoBehaviour
    {
        public StartScreenView StartScreenView;
        public ShopView ShopView;

        public GameObject ShopCanvas;

        private Button _openShopButton;
        private Button _closeShopButton;

        private bool _shopIsOpen;

        private float _screenWidth;

        public void Setup( int highscore)
        {
            _shopIsOpen = false;

            _screenWidth = 2*Camera.main.orthographicSize * Camera.main.aspect;

            MoveShopPanel();



            StartScreenView.SetHighScore(highscore);

            var shopItemModels = new List<PowerupItemShopModel>
            {
                new PowerupItemShopModel(ItemType.Shield),
                new PowerupItemShopModel(ItemType.SpeedBoost),
            };

            ShopView.CreatePowerupItemShopViews(shopItemModels);


            _openShopButton = StartScreenView.OpenShoptButton;
            _openShopButton.onClick.AddListener(MoveShopPanel);
            _closeShopButton = ShopView.CloseShoptButton;
            _closeShopButton.onClick.AddListener(MoveShopPanel);
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
            ShopCanvas.SetActive(false);
        }

        public void ActivateShopCanvas()
        {
            ShopCanvas.SetActive(true);
        }
    }
}