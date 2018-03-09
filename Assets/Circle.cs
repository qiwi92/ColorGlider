using DG.Tweening;
using UnityEngine;

namespace Assets
{ 
    public class Circle : MonoBehaviour
    {
        [HideInInspector] public int Id;
        [HideInInspector] public bool Alive;
        [HideInInspector] public float Speed;
        
        public GameObject GameObject;
        public SpriteRenderer OuterCircleSpriteRenderer;
        public SpriteRenderer FillSpriteRenderer;

        public void SetColor(Color color)
        {
            OuterCircleSpriteRenderer.color = color;
            FillSpriteRenderer.color = color;
        }

        public void SetFill(int id)
        {
            if (id == Id)
            {
                FillSpriteRenderer.DOFade(1, 0.4f);
            }
            else
            {
                FillSpriteRenderer.DOFade(0, 0.6f);
            }
        }
    }
}