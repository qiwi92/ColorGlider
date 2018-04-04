using System.Collections.Generic;
using Assets.Scripts.Powerups;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupShopView : MonoBehaviour
    {
        public Button CloseShoptButton;
        public RectTransform PowerUpShopCanvasTransform;
        [SerializeField] private Transform _powerupItemShopViewParent;
        [SerializeField] private PowerupItemShopView _powerupItemShopViewPrefab;
        [SerializeField] private ColorPalette _colorPalette;
        [SerializeField] private Image _backButtonImage;

        [SerializeField] private Sprite _shieldIcon;
        [SerializeField] private Sprite _boostIcon;

        [SerializeField] public GameObject Panel;

        [SerializeField] private Canvas _canvas;

        public void SetupCamera(Camera cam)
        {
            _canvas.worldCamera = cam;
        }

        private void CreatePowerupItemShopView(PowerupItemShopModel model, Sprite icon, Color color)
        {
            var itemShopView = Instantiate(_powerupItemShopViewPrefab);
            itemShopView.transform.SetParent(_powerupItemShopViewParent,false);

            itemShopView.Setup(model, icon, color);
        }

        public void SetColors(Color color)
        {
            _backButtonImage.color = color;
        }

        public void Setup()
        {
            CreatePowerupItemShopView(new PowerupItemShopModel(PowerupType.Shield, new ShieldData()), _shieldIcon,_colorPalette.PowerupShield);
            CreatePowerupItemShopView(new PowerupItemShopModel(PowerupType.Boost, new BoostData()), _boostIcon,_colorPalette.PowerupBoost);
        }
    }
}