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

        private Color lastColor;



        public ScoreCircleView[] IndicatorImages;
        

        public void SetScore (int score)
        {
            Text.text = score.ToString("0");
        }

        public void SetCounterDots(int index, Color color)
        {
            if (index < 2)
            {
                IndicatorImages[index].FillImage.DOColor(color, 0.2f);
                lastColor = color;
            }
            else if (index == 2)
            {
                IndicatorImages[index].FillImage.DOColor(lastColor, 0.2f).OnComplete(() =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        IndicatorImages[i].FillImage.DOFade(0, 0.2f);
                    }
                });
            }
        }

        public void SetCounterDotsColor( Color color)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IndicatorImages[i].FillImage.color.a > 0.9f)
                {
                    IndicatorImages[i].FillImage.DOColor(color, 0.2f);
                }
            }
        }


        public void EmptyDots()
        {
            for (int i = 0; i < 3; i++)
            {
                IndicatorImages[i].FillImage.DOFade(0, 0.2f);
            }
        }

        public void SetColor(Color color)
        {
            ScoreImage.DOColor(color, 0.4f);

            var colorAlpha = color;
            colorAlpha.a = 0.2f;

            ScoreImageHang.DOColor(colorAlpha, 0.4f);

            for (int i = 0; i < 3; i++)
            {
                IndicatorImages[i].SetColor(color);
            }
        }



      
    }
}
