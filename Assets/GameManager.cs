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
        private int _numberOfCollisions;
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
        public Text HighScore;

 

        public Image UnlockProgressImageRight;
        public Image UnlockProgressImageLeft;
        public Image UnlockProgressFill;
        public Image UnlockProgressFillTriangle;
        public Image RightTutorialArrow;
        public Image LeftTutorialArrow;
        public Text TutorialText;

        public Transform TutoralTexTransform;

        private float _screenWidth;

        private GameState _state;
        private int _highScore;
        private float _currentPressedTime;
        private Color _color;

        public Sounds Sounds;

        

        void Awake ()
        {
            LoadValues();
            HighScore.text = "Highscore: " + _highScore;
            TutorialText.text = Phrases.GetTutorialPhrase();
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;

            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

            CirclesView.SetUp(_screenWidth);

            _collisions = new Collisions();
            _collisions.Circles = CirclesView.Circles;
            _collisions.Glider = Glider;
               
            RighAreaPressed.Action = () => MoveGlider(Direction.Right);
            LeftAreaPressed.Action = () => MoveGlider(Direction.Left);
            
            Setup();
        }

        private void Setup()
        {
            _numberOfCollisions = 0;
            _score = 0;

            GameStateText.text = "New Game";

            Glider.IsAlive = false;
            Glider.ResetPosition();
            SetColors();

            Sounds.PlayDeathTheme(true);
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
                Sounds.PlayDeathTheme(false);
                _state = GameState.Starting;
            }
        }

        private void HandleStartingState()
        {
            _numberOfCollisions = 0;
            _score = 0;

            ScoreView.UpdateHUD(_score,_numberOfCollisions);
            CirclesView.SetParameters(_numberOfCollisions);
            CirclesView.ResetAllPositions();
            
            Glider.IsAlive = true;
            
            Sounds.PlayMainTheme(true);
            Sounds.PlaySartGameSfx();

            SetColors();
            MoveStartPanels();

            GameStateRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
            TutoralTexTransform.DOLocalMove(Vector3.down * 2000, 0.5f);
            PanelImage.DOFade(0, 0.2f);
            
            _state = GameState.Playing;
        }

        private void HandlePlayingState()
        {
            _collisions.CheckCollision();

            var curentCollisions = _collisions.NormalCollections;

            MoveWithArrows();

            if (curentCollisions != _numberOfCollisions && curentCollisions != 0)
            {
                var index = _numberOfCollisions % 3;
                Debug.Log("Index: " + index);
                Sounds.PlayCollectSfx(index);
                _numberOfCollisions = _collisions.NormalCollections;
                _score = _collisions.Score;
                ScoreView.UpdateHUD(_score,_numberOfCollisions);
                CirclesView.SetParameters(_numberOfCollisions);
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
            Glider.ResetPositionSmooth();

            GameStateRectTransform.DOLocalMove(Vector3.up * 200, 0.5f);
            TutoralTexTransform.DOLocalMove(Vector3.down * 550, 0.5f);
            PanelImage.DOFade(0.8f, 0.2f);

            GameStateText.text = "Game Over";

            Sounds.PlayMainTheme(false);
            Sounds.PlayDeathSfx();
            Sounds.PlayDeathTheme(true);

            if (_score > _highScore)
            {
                _highScore = _score;
                SaveValues();

                TutorialText.text = Phrases.GetHighScorePhrase();
            }
            else
            {
                TutorialText.text = Phrases.GetRandomPhrase();
            }

            
            HighScore.text = "Best: " + _highScore;

            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (TwoFingersConfirmation())
            {
                Sounds.PlayDeathTheme(false);
                _state = GameState.Starting;
            }         
        }

        private void SetColors()
        {
            _color = ColorPalette.Colors[Glider.Id];
            Glider.SetColor(_color);
            var emitParams = new ParticleSystem.EmitParams();

            emitParams.position = Vector3.up*4.2f;
            emitParams.applyShapeToPosition = true;
            ScoreView.SetColor(_color);

            LeftAreaImage.color = _color;
            RightAreaImage.color = _color;

            foreach (var scoreCircle in ScoreView.IndicatorImages)
            {
                scoreCircle.SetColor(_color);
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
            var counter = 0;

            if (LeftAreaPressed.IsPressed() || Input.GetKey(KeyCode.LeftArrow))
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth*3.0f/4.0f, 0.4f);
                UnlockProgressImageLeft.DOFillAmount(0.5f, 0.5f).SetEase(Ease.OutExpo);
                LeftTutorialArrow.DOFade(0, 0.4f).SetEase(Ease.OutExpo);
                counter++;
            }

            else
            {
                LeftAreaTransform.transform.DOLocalMoveX(-canvasWidth/4, 0.4f);
                UnlockProgressImageLeft.DOFillAmount(0, 0.5f).SetEase(Ease.OutExpo);
                LeftTutorialArrow.DOFade(1, 0.4f).SetEase(Ease.OutExpo);
            }
            
            if (RighAreaPressed.IsPressed() || Input.GetKey(KeyCode.RightArrow))
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth * 3.0f / 4.0f, 0.4f);
                UnlockProgressImageRight.DOFillAmount(0.5f, 0.5f).SetEase(Ease.OutExpo);
                RightTutorialArrow.DOFade(0, 0.4f).SetEase(Ease.OutExpo);
                counter++;
            }
            else
            {
                RightAreaTransform.transform.DOLocalMoveX(canvasWidth / 4, 0.4f);
                UnlockProgressImageRight.DOFillAmount(0, 0.5f).SetEase(Ease.OutExpo);
                RightTutorialArrow.DOFade(1, 0.4f).SetEase(Ease.OutExpo);
            }

            if (counter == 0)
            {
                GameStateText.text = "Game Over";
                HighScore.DOFade(1, 0.4f);
                UnlockProgressFill.DOFade(0, 0.4f);
                UnlockProgressFillTriangle.DOFade(0, 0.4f);
                TutorialText.DOFade(1, 0.2f);

            }
            else if (counter == 1)
            {
                GameStateText.text = "New Game";
                HighScore.DOFade(0, 0.4f);
                UnlockProgressFill.DOColor(Color.white, 0.4f);
                UnlockProgressFillTriangle.DOFade(1, 0.4f);
                TutorialText.DOFade(1, 0.2f);
            }
            else if (counter == 2)
            {
                GameStateText.text = "Start";
                HighScore.DOFade(0, 0.4f);
                UnlockProgressFill.DOColor(_color, 0.4f);
                UnlockProgressFillTriangle.DOFade(1, 0.4f);
                TutorialText.DOFade(0, 0.2f);

                _currentPressedTime += Time.deltaTime;
            }
            if (counter != 2)
            {
                _currentPressedTime = 0f;
            }

            if (_currentPressedTime > 0.35f)
            {
                _currentPressedTime = 0f;
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


        private void LoadValues()
        {
            _highScore = PlayerPrefs.GetInt("HighScore");
        }

        private void SaveValues()
        {
            PlayerPrefs.SetInt("HighScore",_highScore);
        }

        private void MoveWithArrows()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveGlider(Direction.Left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveGlider(Direction.Right);
            }
        }
    }
}
