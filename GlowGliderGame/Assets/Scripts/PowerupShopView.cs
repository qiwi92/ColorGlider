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

        public void CreatePowerupItemShopViews(List<PowerupItemShopModel> models)
        {
            foreach (var itemShopModel in models)
            {
                var itemShopView = Instantiate(_powerupItemShopViewPrefab);
                itemShopView.transform.SetParent(_powerupItemShopViewParent,false);

                itemShopView.Setup(itemShopModel);
            }
        }
    }
}