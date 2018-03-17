using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreView : MonoBehaviour
    {

        public Text Text;
        public Image ScoreImage;
        public Image ScoreImageHang;
        public Image EnergyImage;



        public ScoreCircleView[] IndicatorImages;
        

        public void UpdateHUD (int score, int numberOfCollision, Color color)
        {
            Text.text = score.ToString("0");

            var indicatorIndex = numberOfCollision % 3;

            for (int i = 0; i < indicatorIndex; i++)
            {
                IndicatorImages[i].Fill(color);
            }

            for (int i = indicatorIndex; i < 3; i++)
            {
                StartCoroutine(IndicatorImages[i].Empty());
            }
        }

        public void SetColor(Color color)
        {
            ScoreImage.DOColor(color, 0.2f);
            EnergyImage.DOColor(color, 0.2f);
            var colorAlpha = color;
            colorAlpha.a = 0.2f;
            ScoreImageHang.DOColor(colorAlpha, 0.2f);

            for (int i = 0; i < 3; i++)
            {
                IndicatorImages[i].SetColor(color);
            }

        }

        public void SetEnergy(float enegry)
        {
            if (enegry < 20)
            {
                EnergyImage.DOFade(0.6f, 0.2f);
            }
            else
            {
                EnergyImage.DOFade(1, 0.2f);
            }
            EnergyImage.DOFillAmount(enegry / 100, 0.2f);
        }


      
    }
}
