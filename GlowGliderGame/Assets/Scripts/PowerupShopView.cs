using System.Collections.Generic;
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
        [SerializeField] private Sprite _icon;
        [SerializeField] private Image _backButtonImage;



        public void CreatePowerupItemShopViews(List<PowerupItemShopModel> models)
        {
            foreach (var itemShopModel in models)
            {
                var itemShopView = Instantiate(_powerupItemShopViewPrefab);
                itemShopView.transform.SetParent(_powerupItemShopViewParent,false);

                itemShopView.Setup(itemShopModel, _icon, _colorPalette.PowerupShield);
            }
        }

        public void SetColors(Color color)
        {
            _backButtonImage.color = color;
        }
    }
}