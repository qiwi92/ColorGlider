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

        public AreaPressed LeftAreaPressed;
        public AreaPressed RighAreaPressed;

        public RectTransform LeftAreaTransform;
        public RectTransform RightAreaTransform;
        public Image RightAreaImage;
        public Image LeftAreaImage;

        public RectTransform MainCanvasTransform;

        public Button PlayButton;
        public Image PanelImage;
        public RectTransform GameStateRectTransform;
        public Text GameStateText;

        public AudioSource MainTheme;
        public AudioSource DeathTheme;
        public AudioSource CollectSound;
        public AudioSource DeathSound;
        public AudioSource StartGameSound;

        private float _screenWidth;



        private bool _gameIsRunning;

        private bool _deathSound = true;

        void Start ()
        {
            _gameIsRunning = false;
            CirclesView.ColorPalette = ColorPalette;

            //float height = Camera.main.orthographicSize;
            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

            CirclesView.SetUp(_screenWidth);

            _collisions = new Collisions();
            _collisions.Circles = CirclesView.Circles;
            _collisions.Glider = Glider;

            Glider.IsAlive = true;
            Glider.ResetPosition();
            SetColors();

            _score = 0;
            CirclesView.Score = _score;
            ScoreView.Score = _score;

            RighAreaPressed.Action = () => MoveGlider(Direction.Right);
            LeftAreaPressed.Action = () => MoveGlider(Direction.Left);

            //PlayButton.onClick.AddListener(StartGame);

            _collisions.CollectSound = CollectSound;
            GameStateText.text = "New Game";
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

            if (!Glider.IsAlive && _gameIsRunning)
            {
                CirclesView.Explode();
                CirclesView.ResetAllPositions();

                _gameIsRunning = false;

                Glider.ResetPositionSmooth();
            }



            if (_gameIsRunning)
            {
                _deathSound = true;
                GameStateText.text = "Game Over";
                GameStateRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
                PlayButton.transform.DOLocalMove(Vector3.down * 2000, 0.5f);
                PanelImage.DOFade(0, 0.2f);
            }
            else
            {
                GameStateRectTransform.DOLocalMove(Vector3.up * 200, 0.5f);
                PlayButton.transform.DOLocalMove(Vector3.zero, 0.2f);
                PanelImage.DOFade(0.8f, 0.2f);
                MainTheme.Stop();


                if (_deathSound)
                {
                    DeathSound.Play();
                    DeathTheme.Play();
                    _deathSound = false;
                }

                TwoFingersConfirmation();

            }
        }

        private void StartGame()
        {
            if (!_gameIsRunning)
            {
                _gameIsRunning = true;
                Glider.IsAlive = true;
                _score = 0;
                MainTheme.Play();
                DeathTheme.Stop();
                StartGameSound.Play();
                SetColors();
                MoveStartPanels();
            }
            
        }

        private void SetColors()
        {
            var color = ColorPalette.Colors[Glider.Id];
            Glider.SetColor(color);
            var emitParams = new ParticleSystem.EmitParams();

            emitParams.position = Vector3.up*4.2f;
            emitParams.applyShapeToPosition = true;
            ScoreView.SetColor(color);

            LeftAreaImage.color = color;
            RightAreaImage.color = color;

            foreach (var scoreCircle in ScoreView.IndicatorImages)
            {
                scoreCircle.SetColor(color);
                StartCoroutine(scoreCircle.Empty());
            }
        }

        private void MoveGlider(Direction direction)
        {
            if (_gameIsRunning)
            {
                Glider.Move(GliderMoveSpeed, _screenWidth, direction);
            }        
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


        private void TwoFingersConfirmation()
        {
            var canvasWidth = MainCanvasTransform.sizeDelta.x;

            if (LeftAreaPressed.IsPressed())
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth*3.0f/4.0f, 0.4f);
            }
            else
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth/4, 0.4f);
            }
            
            if (RighAreaPressed.IsPressed())
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
            }
            else
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth / 4, 0.4f);
            }

            if (LeftAreaPressed.IsPressed() && RighAreaPressed.IsPressed())
            {
                StartGame();
            }

            if (Input.GetKeyDown("space"))
            {
                StartGame();
            }
        }

        private void MoveStartPanels()
        {
            var canvasWidth = MainCanvasTransform.sizeDelta.x;
            LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth * 3.0f / 4.0f, 0.4f);
            RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
        }
      
    }
}
