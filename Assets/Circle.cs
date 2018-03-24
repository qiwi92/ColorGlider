using DG.Tweening;
using UnityEngine;

namespace Assets
{ 
    public class Circle : MonoBehaviour, ICollider
    {
        [HideInInspector] public int Id;
        public bool Alive;
        [HideInInspector] public float Speed;
        [HideInInspector] public int Value;

        private bool _isAlive;

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

        public float GetSize()
        {
            return 0.305f;
        }

        public ObjectType GetType()
        {
            return ObjectType.Circle;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

        public void SetState(bool isAlive)
        {
            _isAlive = isAlive;
        }

        public bool GetState()
        {
            return _isAlive;
        }
    }
}