using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public class CirclesView : MonoBehaviour
    {
        [HideInInspector] public ColorPalette ColorPalette;

        [Range(1, 10)] public float Width;
        
        public float Height;

        [HideInInspector] public int Score;

        private float _speed;

        public Circle CirclePrefab;
        public int CircleAmount;

        public ParticleSystem ParticleSystem;
        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public Circle[] Circles;

        private int _collisionCounter = 0;
        private int _colorCounter = 0;

        public void SetUp()
        {
            _emitParams  = new ParticleSystem.EmitParams();

            _emitParams.applyShapeToPosition = true;

            Circles = new Circle[CircleAmount];

            for (int i = 0; i < CircleAmount; i++)
            {
                var randomX = Random.Range(-Width, Width);
                var randomY = Random.Range(0, 2);

                var randomPos = new Vector3(randomX, Height + randomY, 0);
                var newCircle = Instantiate(CirclePrefab, randomPos, Quaternion.identity);
                newCircle.Alive = true;
                newCircle.Speed = SetSpeed();

                newCircle.Id = (i + 1) % 3;
                newCircle.SetColor(ColorPalette.Colors[newCircle.Id]);

                Circles[i] = newCircle;
            }
        }
	

  


        public void Move()
        {
            foreach (var circle in Circles)
            {
                if (circle.GameObject.transform.position.y < -Height)
                {
                    ResetCircle(circle);
                }
 
                if (!circle.Alive)
                {
                    _emitParams.position = circle.GameObject.transform.position;
                    ParticleSystem.startColor = ColorPalette.Colors[circle.Id];
                    
                    ParticleSystem.Emit(_emitParams,10);

                    ResetCircle(circle);
                    circle.Alive = true;
                    
                }

                circle.GameObject.transform.position += circle.Speed * Time.deltaTime * Vector3.down;
            }
        }


        public void Explode()
        {
            foreach (var circle in Circles)
            {
                if (!circle.Alive)
                {
                    _emitParams.position = circle.GameObject.transform.position;
                    ParticleSystem.startColor = ColorPalette.Colors[circle.Id];

                    ParticleSystem.Emit(_emitParams, 10);
                }
            }
        }




        public void ResetAllPositions()
        {
            foreach (var circle in Circles)
            {
                ResetCircle(circle);
            }
        }


        private void ResetCircle(Circle circle)
        {
            var randomX = Random.Range(-Width, Width);
            var randomY = Random.Range(0, Height);

            circle.GameObject.transform.position = new Vector3(randomX, Height + randomY, 0);
            circle.Speed = SetSpeed();
        }

        private float SetSpeed()
        {
            var baseSpeed = 3 + 0.1f * Score;
            return _speed = Random.Range(baseSpeed, baseSpeed * 1.3f);
        }
    }
}