using Assets.Scripts.Powerups;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class CirclesView : MonoBehaviour
    {
        [HideInInspector] public ColorPalette ColorPalette;

        private float _width;
        
        private readonly SpeedData _speedData = new SpeedData();

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
                newCircle.RandomFactor = Random.Range(1, 1.1f);

                newCircle.Id = (i + 1) % 3;
                newCircle.SetColor(ColorPalette.Colors[newCircle.Id]);

                Circles[i] = newCircle;
            }
        }

        public void Move()
        {
            var smoothDeltaTime = Time.smoothDeltaTime;

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
                circle.transform.position += circle.Speed * smoothDeltaTime * Vector3.down;
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
            circle.SetFill(ColorPalette.Colors[circle.Id]);

            var spawnFilled = _score >= 9;
            circle.SetValue(spawnFilled);
        }

        public void SetSpeed(int score, bool isBoosted)
        {
            _score = score;

            foreach (var circle in Circles)
            {
                circle.Speed = _speedData.GetSpeed(score, isBoosted) * circle.RandomFactor;
            }
        }

        public void SetSpeedFactor(float speedFactor)
        {
            foreach (var circle in Circles)
            {
                circle.Speed = _speed * speedFactor;
            }
        }

        public void ResetSpeed()
        {
            foreach (var circle in Circles)
            {
                circle.Speed = circle.OldSpeed;
            }
        }
    }

    public interface IItemSpeed
    {
        float SetSpeedForReset(int score);
        void SetSpeedFactor(float speedFactor);
        void ResetSpeed();
    }
}