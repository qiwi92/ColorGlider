using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreView : MonoBehaviour
    {

        public Text Text;
        public Image Image;
        public Collisions Collisions;
        

        void Update ()
        {
            Text.text = GetScore().ToString("0");   
        }

        public void SetColor(Color color)
        {
            Image.color = color;
        }

        public int GetScore()
        {
            return Collisions.NumberOfCollisions;
        }
    }
}
