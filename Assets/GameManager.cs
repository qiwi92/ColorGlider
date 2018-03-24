using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets
{
    
    public class GameManager : MonoBehaviour
    {
        public InputController InputController;
        public ColorPalette ColorPalette;
        public CirclesView CirclesView;
        public Glider Glider;
        private Collisions _collisions;
        public ScoreView ScoreView;
        private int _numberOfCollisions;


        public float GliderMoveSpeed;

        



        public RectTransform MainCanvasTransform;

        public Image PanelImage;
        public RectTransform GameStateRectTransform;
        

 

        

        public Transform TutoralTexTransform;

        private float _screenWidth;

        private GameState _state;
        private int _highScore;
        
        private Color _color;

        public Sounds Sounds;
        private Color _currentColor;


        void Awake ()
        {
            LoadValues();
            
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;
            Glider.ColorPalette = ColorPalette;
            Glider.Sounds = Sounds;


            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

            CirclesView.SetUp(_screenWidth);

            _collisions = new Collisions(CirclesView.Circles, new Diamond[0], new PowerUp[0] );
            _collisions.Glider = Glider;
            Glider.Id = 0;

            _color = ColorPalette.Colors[Glider.Id];

            
            
            Setup();
        }

        private void Setup()
        {
            _numberOfCollisions = 0;

            InputController.GameStateText.text = "New Game";
            InputController.HighScore.text = "Highscore: " + _highScore;
            InputController.TutorialText.text = Phrases.GetTutorialPhrase();
            InputController.RighAreaPressed.Action = () => MoveGlider(Direction.Right);
            InputController.LeftAreaPressed.Action = () => MoveGlider(Direction.Left);
            InputController.SetColors(_color);

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
            if (InputController.TwoFingersConfirmation())
            {
                Sounds.PlayDeathTheme(false);
                _state = GameState.Starting;
            }
        }

        private void HandleStartingState()
        {
            _numberOfCollisions = 0;
            Glider.Score = 0;

            
            CirclesView.SetSpeed(_numberOfCollisions);
            CirclesView.ResetAllPositions();
            
            Glider.IsAlive = true;
            ScoreView.SetScore(Glider.Score);
            ScoreView.EmptyDots();

            Sounds.PlayMainTheme(true);
            Sounds.PlaySartGameSfx();

            SetColors();
            InputController.MoveStartPanels();

            GameStateRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
            TutoralTexTransform.DOLocalMove(Vector3.down * 2000, 0.5f);
            PanelImage.DOFade(0, 0.2f);

            _state = GameState.Playing;
        }

        private void HandlePlayingState()
        {
            _collisions.CheckCollisions();

            MoveWithArrows();

            if (Glider.CollisionState == CollisionStates.JustCollided)
            {
                ScoreView.SetScore(Glider.Score);
                CirclesView.SetSpeed(Glider.Score);
                SetColors();
                SetCounterDots();

                Glider.CollisionState = CollisionStates.None;
            }

            CirclesView.Move();

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

            InputController.GameStateText.text = "Game Over";

            Sounds.PlayMainTheme(false);
            Sounds.PlayDeathSfx();
            Sounds.PlayDeathTheme(true);

            if (Glider.Score > _highScore)
            {
                _highScore = Glider.Score;
                SaveValues();

                InputController.TutorialText.text = Phrases.GetHighScorePhrase();
            }
            else
            {
                InputController.TutorialText.text = Phrases.GetRandomPhrase();
            }

            InputController.SetColors(_color);
            InputController.HighScore.text = "Best: " + _highScore;

            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (InputController.TwoFingersConfirmation())
            {
                Sounds.PlayDeathTheme(false);
                _state = GameState.Starting;
            }         
        }

        private void SetColors()
        {
            _color = ColorPalette.Colors[Glider.Id];
            ScoreView.SetColor(_color);
        }

        private void SetCounterDots()
        {
            ScoreView.SetCounterDots(Glider.Index, _color);
        }

        private void MoveGlider(Direction direction)
        {
            if (_state == GameState.Playing)
            {
                Glider.Move(GliderMoveSpeed, _screenWidth, direction);
            }  
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


        //private bool TwoFingerPressed()
        //{
        //    if ((InputController.LeftAreaPressed.IsPressed() || Input.GetKey(KeyCode.LeftArrow)) &&
        //        (InputController.RighAreaPressed.IsPressed() || Input.GetKey(KeyCode.RightArrow)))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
