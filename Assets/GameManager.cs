using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public Image PanelImage;
        public RectTransform GameStateRectTransform;
        public Text GameStateText;

        public AudioSource MainTheme;
        public AudioSource DeathTheme;
        public AudioSource CollectSound;
        public AudioSource DeathSound;
        public AudioSource StartGameSound;

        private float _screenWidth;

        private GameState _state;

        void Awake ()
        {
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;

            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

            CirclesView.SetUp(_screenWidth);

            _collisions = new Collisions();
            _collisions.Circles = CirclesView.Circles;
            _collisions.Glider = Glider;
               
            RighAreaPressed.Action = () => MoveGlider(Direction.Right);
            LeftAreaPressed.Action = () => MoveGlider(Direction.Left);

            _collisions.CollectSound = CollectSound;
        
            Setup();
        }

        private void Setup()
        {
            _score = 0;

            GameStateText.text = "New Game";

            Glider.IsAlive = false;
            Glider.ResetPosition();
            SetColors();
            DeathTheme.Play();
        }

        private enum GameState
        {
            Init,
            Starting,
            Playing,
            Dying,
            Dead
        }

        private void Update()
        {
            switch (_state)
            {
                case GameState.Init:
                    HandleInitState();
                    break;
                case GameState.Starting:
                    HandleStartingState();
                    break;
                case GameState.Playing:
                    HandlePlayingState();
                    break;
                case GameState.Dying:
                    HandleDyingState();
                    break;
                case GameState.Dead:
                    HandleDeadState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleInitState()
        {
            if (TwoFingersConfirmation())
            {
                DeathTheme.Stop();
                CirclesView.SetSpeed(_score);
                CirclesView.ResetAllPositions();

                _state = GameState.Starting;
            }
        }

        private void HandleStartingState()
        {
            _score = 0;

            CirclesView.SetSpeed(_score);
            ScoreView.UpdateHUD(_score);

            Glider.IsAlive = true;
            
            MainTheme.Play();
            
            StartGameSound.Play();
            SetColors();
            MoveStartPanels();

            GameStateRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
            PanelImage.DOFade(0, 0.2f);
            
            _state = GameState.Playing;
        }

        private void HandlePlayingState()
        {
            _collisions.CheckCollision();

            var curentScore = _collisions.NumberOfCollisions;
            
            if (curentScore != _score)
            {
                _score = _collisions.NumberOfCollisions;
                ScoreView.UpdateHUD(_score);
                CirclesView.SetSpeed(_score);
            }

            CirclesView.Move();
            SwitchColors();

            if (!Glider.IsAlive)
            {
                _state = GameState.Dying;
            }
        }

        private void HandleDyingState()
        {
            CirclesView.Explode();
            CirclesView.ResetAllPositions();
            Glider.ResetPositionSmooth();

            GameStateRectTransform.DOLocalMove(Vector3.up * 200, 0.5f);
            PanelImage.DOFade(0.8f, 0.2f);

            GameStateText.text = "Game Over";

            MainTheme.Stop();
            DeathSound.Play();
            DeathTheme.Play();

            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (TwoFingersConfirmation())
            {
                DeathTheme.Stop();
                _state = GameState.Starting;
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
            if (_state == GameState.Playing)
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

        private bool TwoFingersConfirmation()
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
                return true;
            }

            if (Input.GetKeyDown("space"))
            {
                return true;
            }

            return false;
        }

        private void MoveStartPanels()
        {
            var canvasWidth = MainCanvasTransform.sizeDelta.x;
            LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth * 3.0f / 4.0f, 0.4f);
            RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
        }
      
    }
}
