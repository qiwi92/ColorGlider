using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupProgressView : MonoBehaviour
    {
        [SerializeField] private Image[] _progressImages;
        private Color _color;

        public void Init(Color color)
        {
            _color = color;
            foreach (var progressImage in _progressImages)
            {
                progressImage.color = Color.black;               
            }
        }

        public void SetProgress(int current)
        {
            for (int i = 0; i < current; i++)
            {
                _progressImages[i].color = _color;
            }
        }
    }
}