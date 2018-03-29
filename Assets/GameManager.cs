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
        public DiamondsView DiamondsView;
        public PowerupsView PowerupsView;
        public Glider Glider;
        private Collisions _collisions;
        public ScoreView ScoreView;
        private int _numberOfCollisions;


        public float GliderMoveSpeed;

        public Image PanelImage;
        public RectTransform GameStateRectTransform;
        
        public Transform TutoralTexTransform;

        private float _screenWidth;

        private GameState _state;
        private int _highScore;
        
        private Color _color;

        private Sounds _sounds;
        private Color _currentColor;


        void Awake ()
        {
            _sounds = FindObjectOfType<Sounds>();
            
            LoadValues();
            
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;
            Glider.ColorPalette = ColorPalette;
            Glider.Sounds = _sounds;


            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

            
            

            Glider.CirclesView = CirclesView;
            Glider.DiamondsView = DiamondsView;
            Glider.PowerupsView = PowerupsView;

            CirclesView.SetUp(_screenWidth);
            DiamondsView.ColorPalette = ColorPalette;
            DiamondsView.SetUp(_screenWidth);
            PowerupsView.ColorPalette = ColorPalette;
            PowerupsView.SetUp(_screenWidth);

            Glider.Setup();

            Glider.Id = 0;

            _color = ColorPalette.Colors[Glider.Id];

            InputController.GliderTransform = Glider.transform;

            
            Setup();


        }

        private void Setup()
        {
            _numberOfCollisions = 0;
           
            Glider.IsAlive = false;
            Glider.ResetPosition();
            SetColors();
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

            //if (InputController.PlayButton.GetState())
            //{
                _state = GameState.Starting;
            //}
        }

        private void HandleStartingState()
        {

            Glider.Score = 0;        
            CirclesView.SetSpeed(0);
            CirclesView.ResetAllPositions();
            
            Glider.IsAlive = true;
            ScoreView.SetScore(Glider.Score);
            ScoreView.EmptyDots();

            _sounds.PlayMainTheme(true);
            _sounds.PlaySartGameSfx();

            SetColors();

            GameStateRectTransform.DOLocalMove(Vector3.up * 2000, 0.5f);
            TutoralTexTransform.DOLocalMove(Vector3.down * 2000, 0.5f);
            PanelImage.DOFade(0, 0.2f);

            _state = GameState.Playing;
        }

        private void HandlePlayingState()
        {
            Glider.Collisions.CheckCollisions();
            Glider.Move(GliderMoveSpeed, _screenWidth,InputController.GetMoveDirection());

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
            DiamondsView.Move();
            PowerupsView.Move();

            if (!Glider.IsAlive)
            {
                _state = GameState.Dying;
            }


        }

        private void HandleDyingState()
        {
            InputController.PlayButton.SetStateToNotPlaying();
            Glider.ResetPositionSmooth();

            GameStateRectTransform.DOLocalMove(Vector3.up * 200, 0.5f);
            TutoralTexTransform.DOLocalMove(Vector3.down * 550, 0.5f);
            PanelImage.DOFade(0.8f, 0.2f);

            //InputController.GameStateText.text = "Game Over";

            _sounds.PlayMainTheme(false);
            _sounds.PlayDeathSfx();
            _sounds.PlayDeathTheme(true);

            if (Glider.Score > _highScore)
            {
                _highScore = Glider.Score;
                SaveValues();
            }
       

            //InputController.SetColors(_color);
            //InputController.HighScore.text = "Best: " + _highScore;

            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (InputController.PlayButton.GetState())
            {
                _sounds.PlayDeathTheme(false);
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
            Glider.Money = PlayerPrefs.GetInt("Money");
        }

        private void SaveValues()
        {
            PlayerPrefs.SetInt("HighScore",_highScore);
            PlayerPrefs.SetInt("Money",Glider.Money);
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
