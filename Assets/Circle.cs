using DG.Tweening;
using UnityEngine;

namespace Assets
{ 
    public class Circle : MonoBehaviour
    {
        [HideInInspector] public int Id;
        [HideInInspector] public bool Alive;
        [HideInInspector] public float Speed;
        [HideInInspector] public int Value;
        
        public SpriteRenderer OuterCircleSpriteRenderer;
        public SpriteRenderer FillSpriteRenderer;

        public void SetColor(Color color)
        {
            OuterCircleSpriteRenderer.color = color;
            FillSpriteRenderer.color = color;
        }

        public void SetFill()
        {
            if (Value >= 2)
            {
                FillSpriteRenderer.DOFade(1, 0.4f);
            }
            else
            {
                FillSpriteRenderer.DOFade(0, 0.6f);
            }
        }

        public void SetValue(int score)
        {
            if (score <= 9)
            {
                Value = 1;
            }
            else
            {
                var randomValue = Random.Range(1, 3);
                Value = randomValue;
            }         
        }
    }
}