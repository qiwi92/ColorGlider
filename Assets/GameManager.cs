using UnityEngine;

namespace Assets
{
    public class GameManager : MonoBehaviour
    {
        public ColorPalette ColorPalette;
        public CirclesView CirclesView;
        public Glider Glider;
        private Collisions _collisions;
        public ScoreView ScoreView;

        void Start ()
        {
            CirclesView.ColorPalette = ColorPalette;
            CirclesView.SetUp();
            CirclesView.Score = 0;
            

            _collisions = new Collisions();
            _collisions.Circles = CirclesView.Circles;
            _collisions.Glider = Glider;

            SetColors();


            ScoreView.Collisions = _collisions;
        }

        void Update()
        {
            _collisions.CheckCollision();
            CirclesView.Score = ScoreView.GetScore();

            if (_collisions.SwitchColor)
            {
                var previousId = Glider.Id;

                var randomId = (int)Random.Range(0, 3);
                Glider.Id = randomId;


                if (previousId == randomId)
                {
                    Glider.Id = (previousId + 1) % 3;
                }

                SetColors();


                _collisions.SwitchColor = false;
            }
        }

        private void SetColors()
        {
            var color = ColorPalette.Colors[Glider.Id];
            Glider.SetColor(color);
            var emitParams = new ParticleSystem.EmitParams();

            emitParams.position = Vector3.up*4.2f;
            emitParams.applyShapeToPosition = true;
            

            Glider.SwitchParticleSystem.Emit(emitParams,20);
            Glider.SwitchParticleSystemBlured.Emit(30);
            ScoreView.SetColor(color);
        }

       
    }
}
