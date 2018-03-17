using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreCircleView : MonoBehaviour
    {

        public Image CircleImage;
        public Image FillImage;

        public void SetColor(Color color)
        {
            CircleImage.DOColor(color, 0.2f);
        }

        public void Fill(Color color)
        {
            FillImage.DOColor(color, 0.2f);
        }

        public IEnumerator Empty()
        {
            yield return new WaitForSeconds(0.2f);
            FillImage.DOFade(0, 0.2f);
            
        }
    }
}
