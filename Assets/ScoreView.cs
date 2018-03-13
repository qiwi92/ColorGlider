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



        public ScoreCircleView[] IndicatorImages;
        

        public void UpdateHUD (int score)
        {
            Text.text = score.ToString("0");

            var indicatorIndex = score % 3;
            for (int i = 0; i < indicatorIndex; i++)
            {
                IndicatorImages[i].Fill();
            }
        }

        public void SetColor(Color color)
        {
            ScoreImage.DOColor(color, 0.2f);
            var colorAlpha = color;
            colorAlpha.a = 0.2f;
            ScoreImageHang.DOColor(colorAlpha, 0.2f);
        } 


    }
}
