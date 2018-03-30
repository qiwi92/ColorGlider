using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupIconView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _outlineImage;

        public void SetImage(Sprite icon, Color color)
        {
            _outlineImage.color = color;
            _iconImage.color = color;
            _iconImage.sprite = icon;
        }
    }
}