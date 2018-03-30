using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class CirclesView : MonoBehaviour 
    {
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        
        public float Height;
        private float _speed;

        public CircleView CirclePrefab;
        public int CircleAmount;

        public ParticleSystem ParticleSystem;
        private ParticleSystem.EmitParams _emitParams;

        [HideInInspector] public CircleView[] Circles;

        private int _score;


        public void SetUp(float width)
        {
            _score = 0;
            _width = width;
            _emitParams  = new ParticleSystem.EmitParams();

            _emitParams.applyShapeToPosition = true;

            Circles = new CircleView[CircleAmount];

            for (int i = 0; i < CircleAmount; i++)
            {
                var randomX = Random.Range(-_width, _width);
                var randomY = Random.Range(0, 2);

                var randomPos = new Vector3(randomX, Height + randomY, 0);
                var newCircle = Instantiate(CirclePrefab, randomPos, Quaternion.identity);
                newCircle.Alive = true;
                newCircle.Speed = _speed;

                newCircle.Id = (i + 1) % 3;
                newCircle.SetColor(ColorPalette.Colors[newCircle.Id]);

                Circles[i] = newCircle;
            }
        }

        public void Move()
        {
            foreach (var circle in Circles)
            {
                if (circle.transform.position.y < -Height)
                {
                    ResetCircle(circle);
                }
 
                if (!circle.Alive)
                {
                    _emitParams.position = circle.transform.position;
                    ParticleSystem.startColor = ColorPalette.Colors[circle.Id];
                    
                    ParticleSystem.Emit(_emitParams,10);

                    ResetCircle(circle);
                    circle.Alive = true;
                    
                }

                circle.transform.position += circle.Speed * Time.smoothDeltaTime * Vector3.down;
            }
        }

        public void Explode()
        {
            foreach (var circle in Circles)
            {
                if (!circle.Alive)
                {
                    _emitParams.position = circle.transform.position;
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

        public void KillAll()
        {
            foreach (var circle in Circles)
            {
                circle.Alive = false;
                _emitParams.position = circle.transform.position;
                ParticleSystem.startColor = ColorPalette.Colors[circle.Id];

                ParticleSystem.Emit(_emitParams, 10);
                ResetCircle(circle);
            }
        }

        private void ResetCircle(CircleView circle)
        {
            var randomX = Random.Range(-_width, _width);
            var randomY = Random.Range(0, 2*Height);

            circle.transform.position = new Vector3(randomX, Height + randomY, 0);
            circle.Speed = _speed;
            circle.SetFill(ColorPalette.Colors[circle.Id]);
            circle.SetValue(_score);
        }

        public float SetSpeed(int score)
        {
            _score = score;
            var baseSpeed = 3 + 0.1f * score;
            return _speed = Random.Range(baseSpeed, baseSpeed * 1.3f);
        }
    }
}