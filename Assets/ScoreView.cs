using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreView : MonoBehaviour
    {

        public Text Text;
        public Image Image;
        public int Score;
        

        void Update ()
        {
            Text.text = Score.ToString("0");   
        }

        public void SetColor(Color color)
        {
            Image.color = color;
        } 
    }
}
