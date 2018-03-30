using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupProgressView : MonoBehaviour
    {
        [SerializeField]
        private Image[] _progressImages;

        public void Init()
        {
            foreach (var progressImage in _progressImages)
            {
                progressImage.color = Color.black;               
            }
        }

        public void SetProgress(int current)
        {
            for (int i = 0; i < current; i++)
            {
                _progressImages[i].color = Color.green;
            }
        }
    }
}