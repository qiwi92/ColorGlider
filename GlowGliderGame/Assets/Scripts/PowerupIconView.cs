using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupIconView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _outlineImage;

        public void SetImage(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
    }
}