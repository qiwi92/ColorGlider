using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainView : MonoBehaviour
    {
        public StartScreenView StartScreenView;
        public PowerupShopView PowerupShopView;

        private Button _openShopButton;
        private Button _closeShopButton;

        private bool _shopIsOpen;

        private float _screenWidth;

        public void Setup(int highscore)
        {
            _shopIsOpen = false;

            _screenWidth = PowerupShopView.PowerUpShopCanvasTransform.rect.width;
            PowerupShopView.PowerUpShopCanvasTransform.DOLocalMove(Vector3.right * _screenWidth, 0.4f);


            StartScreenView.SetHighScore(highscore);

            var shopItemModels = new List<PowerupItemShopModel>
            {
                new PowerupItemShopModel(ItemType.Shield),
                new PowerupItemShopModel(ItemType.SpeedBoost),
            };

            PowerupShopView.CreatePowerupItemShopViews(shopItemModels);


            _openShopButton = StartScreenView.OpenShoptButton;
            _openShopButton.onClick.AddListener(MoveShopPanel);
            _closeShopButton = PowerupShopView.CloseShoptButton;
            _closeShopButton.onClick.AddListener(MoveShopPanel);
        }

        private void MoveShopPanel()
        {
            if (_shopIsOpen)
            {
                PowerupShopView.PowerUpShopCanvasTransform.DOLocalMove(Vector3.zero, 0.4f);
                _shopIsOpen = false;
            }
            else
            {
                PowerupShopView.PowerUpShopCanvasTransform.DOLocalMove(Vector3.right * _screenWidth, 0.4f);
                _shopIsOpen = true;
            }
        }
    }
}