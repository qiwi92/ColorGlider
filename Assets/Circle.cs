using UnityEngine;

namespace Assets
{ 
    public class Circle : MonoBehaviour
    {
        [HideInInspector] public int Id;
        [HideInInspector] public bool Alive;
        [HideInInspector] public float Speed;
        public GameObject GameObject;
        public SpriteRenderer SpriteRenderer;

        public void SetColor(Color color)
        {
            SpriteRenderer.color = color;
        }
    }
}