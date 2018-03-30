using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "ScriptableObjects/ColorPalette")]
    public class ColorPalette : ScriptableObject
    {
        public Color[] Colors;
        public Color Untargetable;
        public Color Diamond;
        public Color PowerupBoost;
        public Color PowerupShield;
    }
}