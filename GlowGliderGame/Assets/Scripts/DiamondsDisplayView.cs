using DG.Tweening;
using Money;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DiamondsDisplayView : MonoBehaviour , IObserver
    {
        public Image DiamondsImage;
        public Text DiamondText;

        private void Start()
        {
            MoneyService.Instance.AddObserver(this);

            DiamondText.DOFade(0, 0.2f);
            DiamondsImage.DOFade(0, 0.2f);
        }

        private void SetNewValue(int diamondAmount)
        {
            DiamondText.text = diamondAmount.ToString("0");
            DiamondText.DOFade(1, 0.2f);
            DiamondsImage.DOFade(1, 0.2f).OnComplete(() =>
            {
                DiamondText.DOFade(0, 2f);
                DiamondsImage.DOFade(0, 2f);
            });
        }

        
        public void NotifyChange(int value)
        {
            SetNewValue( value);
        }
    }
}