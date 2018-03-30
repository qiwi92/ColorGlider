using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreCircleView : MonoBehaviour
    {

        public Image CircleImage;
        public Image FillImage;

   
        public void SetColor(Color color)
        {
            CircleImage.DOColor(color, 0.2f);
        }

        public void Fill()
        {
            FillImage.DOFade(1, 0.2f);
        }

        public void Empty()
        {
            FillImage.DOFade(0, 0.2f);     
        }
    }
}
