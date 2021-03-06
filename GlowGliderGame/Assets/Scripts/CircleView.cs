﻿using UnityEngine;

namespace Assets.Scripts
{ 
    public class CircleView : MonoBehaviour, ICollider
    {
        [HideInInspector] public int Id;
        [HideInInspector] public bool Alive;
        [HideInInspector] public float Speed;
        [HideInInspector] public float RandomFactor;
        [HideInInspector] public float OldSpeed;
        [HideInInspector] public int Value;

        private bool _isAlive;

        private float _speed;
  


        public SpriteRenderer OuterCircleSpriteRenderer;
        public SpriteRenderer FillSpriteRenderer;

        public void SetColor(Color color)
        {
            OuterCircleSpriteRenderer.color = color;
            FillSpriteRenderer.color = color;
        }

        private void SetFill(Color color)
        {
            if (Value >= 2)
            {
                FillSpriteRenderer.color = color;
            }
            else
            {
                FillSpriteRenderer.color = new Color(0, 0, 0, 0);
            }
        }

        public void SetValue(bool canSpawnFilled, Color color)
        {
            if (!canSpawnFilled)
            {
                Value = 1;
            }
            else
            {
                var randomValue = Random.Range(1, 3);
                Value = randomValue;
            }

            SetFill(color);
        }

        public float GetSize()
        {
            return 0.305f;
        }

        public ObjectType GetObjectType()
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