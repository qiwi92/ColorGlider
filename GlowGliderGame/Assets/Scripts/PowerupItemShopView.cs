using Money;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupItemShopView : MonoBehaviour, IObserver
    {
        [SerializeField] private PowerupProgressView _powerupProgressView;
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _cost;
        [SerializeField] private Text _buttonLabel;
        [SerializeField] private Button _buyButton;
        [SerializeField] private ColorPalette _colorPalette;

        private PowerupItemShopModel _itemShopModel;

        public void Setup(PowerupItemShopModel model)
        {
            MoneyService.Instance.AddObserver(this);

            _itemShopModel = model;
            _itemName.text = _itemShopModel.Name;
            _powerupProgressView.Init();

            _buyButton.onClick.AddListener(TryBuyPowerup);

            UpdateView();
        }

        private void UpdateView()
        {
            SetButtonLabel();
            _powerupProgressView.SetProgress(_itemShopModel.Level);
            _cost.text = _itemShopModel.Cost.ToString("0");
        }

        private void SetButtonLabel()
        {
            if (_itemShopModel.IsMaxLevel)
            {
                _buttonLabel.text = "Max";
                return;
            }

            _buttonLabel.text = _itemShopModel.IsUnlocked ? "Upgrade" : "Unlock";
        }

        private void TryBuyPowerup()
        {
            _itemShopModel.TryBuy();
            UpdateView();
            UpdateButtonState();
        }

        public void NotifyChange(int value)
        {
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            _buyButton.interactable = _itemShopModel.CanBuy();
        }
    }
}