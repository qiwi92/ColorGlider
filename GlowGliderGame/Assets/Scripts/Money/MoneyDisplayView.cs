using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Money
{
    public class MoneyDisplayView : MonoBehaviour, IObserver
    {
        public Text MoneyDisplayText;

        private void Start()
        {
            MoneyService.Instance.AddObserver(this);
        }

        public void NotifyChange(int value)
        {
            MoneyDisplayText.text = value.ToString("0");
        }
    }
}