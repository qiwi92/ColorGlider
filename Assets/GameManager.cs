using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class GameManager : MonoBehaviour
    {
        public ColorPalette ColorPalette;
        public CirclesView CirclesView;
        public Glider Glider;
        private Collisions _collisions;
        public ScoreView ScoreView;
        private int _score;


        public float GliderMoveSpeed;
        public float MaxGliderRange;

        public ButtonPressed LeftButtonPressed;
        public ButtonPressed RighButtonPressed;

        public Button PlayButton;
        public Image PanelImage;
        public RectTransform GameOverRectTransform;


        private bool _gameIsRunning;

        void Start ()
        {
            _gameIsRunning = true;
            CirclesView.ColorPalette = ColorPalette;
            CirclesView.SetUp();
            
            

            _collisions = new Collisions();
            _collisions.Circles = CirclesView.Circles;
            _collisions.Glider = Glider;

            Glider.IsAlive = true;
            Glider.ResetPosition();
            SetColors();

            _score = 0;
            CirclesView.Score = _score;
            ScoreView.Score = _score;

            RighButtonPressed.Action = () => MoveGlider(Direction.Right);
            LeftButtonPressed.Action = () => MoveGlider(Direction.Left);

            PlayButton.onClick.AddListener(StartGame);
        }

        void Update()
        {

            _collisions.CheckCollision();

            if (Glider.IsAlive && _gameIsRunning)
            {
                _score = _collisions.NumberOfCollisions;
                ScoreView.Score = _score;
                CirclesView.Score = _score;
                CirclesView.Move();
                SwitchColors();
            }
            if(!Glider.IsAlive && _gameIsRunning)
            {
                CirclesView.Explode();
                CirclesView.ResetAllPositions();

                _gameIsRunning = false;

                Glider.ResetPositionSmooth();
            }

            if (_gameIsRunning)
            {
                GameOverRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
                PlayButton.transform.DOLocalMove(Vector3.down * 2000, 0.5f);
                PanelImage.DOFade(0, 0.2f);
            }
            else
            {
                GameOverRectTransform.DOLocalMove(Vector3.up * 200, 0.5f);
                PlayButton.transform.DOLocalMove(Vector3.zero, 0.2f);
                PanelImage.DOFade(0.8f, 0.2f);
            }


            


        }

        private void StartGame()
        {
            if (!_gameIsRunning)
            {
                _gameIsRunning = true;
                Glider.IsAlive = true;
                _score = 0;
            }
            
        }

        private IEnumerator GameOver()
        {
            PlayButton.transform.DOLocalMove(Vector3.zero, 0.2f);
            yield return new WaitForSeconds(0.2f);

        }

        private void SetColors()
        {
            var color = ColorPalette.Colors[Glider.Id];
            Glider.SetColor(color);
            var emitParams = new ParticleSystem.EmitParams();

            emitParams.position = Vector3.up*4.2f;
            emitParams.applyShapeToPosition = true;
            ScoreView.SetColor(color);
        }

        private void MoveGlider(Direction direction)
        {
            Glider.Move(GliderMoveSpeed, MaxGliderRange,direction);
        }

        private void SwitchColors()
        {
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
    }
}
