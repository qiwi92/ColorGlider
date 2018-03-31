using System;
using System.Net;
using Analytics;
using Assets.Scripts.Powerups;
using DG.Tweening;
using Highscore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public MainView MainView;
        public InputController InputController;
        public ColorPalette ColorPalette;
        public CirclesView CirclesView;
        public DiamondsView DiamondsView;
        public PowerupsView PowerupsView;
        public Glider Glider;

        public ScoreView ScoreView;


        public float GliderMoveSpeed;

        public Image PanelImage;

        
        private float _screenWidth;

        private GameState _state;
        private BoostState _boostState;
        private int _highScore;
        
        private Color _color;

        public Sounds Sounds;

        private readonly AnalyticsApiModel _analyticsApi = new AnalyticsApiModel();
        private readonly GuidProvider _guidProvider = new GuidProvider();
        private float _gameDuration;

        void Awake ()
        {
            if(Debug.isDebugBuild)
               SRDebug.Init();

            ServicePointManager.ServerCertificateValidationCallback = ServerUtils.MyRemoteCertificateValidationCallback;
            LoadValues();

            _boostState = BoostState.None;
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;
            Glider.ColorPalette = ColorPalette;
            Glider.Sounds = Sounds;

            _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            MainView.Setup(_highScore);

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
            MainView.SetHighScoreButtonState(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerAlias")));

            Setup();
        }

        private void Setup()
        {
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
            if (MainView.StartScreenView.PlayButton.GetState())
            {
                _state = GameState.Starting;
            }
        }

        private void HandleStartingState()
        {
            _gameDuration = 0;
            MainView.StartScreenView.PlayAnimation();
            MainView.DeactivateShopCanvas();
            MainView.DeactivateScoreCanvas();

            Glider.Score = 0;        
            SetSpeeds(false);

            CirclesView.ResetAllPositions();
            
            Glider.IsAlive = true;
            ScoreView.SetScore(Glider.Score);
            ScoreView.EmptyDots();

            Sounds.PlayMainTheme(true);
            Sounds.PlaySartGameSfx();

            SetColors();

            PowerupsView.SetStartValues();
            
            PanelImage.DOFade(0, 0.2f);

            _state = GameState.Playing;
        }

        private void HandlePlayingState()
        {
            _gameDuration += Time.deltaTime;

            Glider.Collisions.CheckCollisions();
            Glider.Move(GliderMoveSpeed, _screenWidth,InputController.GetMoveDirection());

            MoveWithArrows();

            if (Glider.CollisionState == CollisionStates.JustCollided)
            {
                ScoreView.SetScore(Glider.Score);
                CirclesView.SetSpeed(Glider.Score,Glider.IsBoosted);
                DiamondsView.SetSpeed(Glider.Score, Glider.IsBoosted);
                PowerupsView.SetSpeed(Glider.Score, Glider.IsBoosted);
                SetColors();
                SetCounterDots();

                Glider.CollisionState = CollisionStates.None;
            }

            switch (_boostState)
            {
                case BoostState.None:
                    if (Glider.IsBoosted)
                    {
                        _boostState = BoostState.StartingBoost;
                    }
                    break;
                case BoostState.StartingBoost:
                    SetSpeeds(true);
                    _boostState = BoostState.Boosted;
                    break;
                case BoostState.Boosted:
                    if (!Glider.IsBoosted)
                    {
                        _boostState = BoostState.StoppingBoost;
                    }
                    break;
                case BoostState.StoppingBoost:
                    SetSpeeds(false);
                    _boostState = BoostState.None;
                    CirclesView.KillAll();
                    break;
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
            _analyticsApi.TrackSession((ushort) _gameDuration, _guidProvider.GetGuid().ToString(),(ushort) _highScore);
            MainView.ActivateShopCanvas();
            MainView.ActivateScoreCanvas();
            Glider.ResetPositionSmooth();
            
            PanelImage.DOFade(0.8f, 0.2f);

            Sounds.PlayMainTheme(false);
            Sounds.PlayDeathSfx();
            Sounds.PlayDeathTheme(true);

            if (Glider.Score > _highScore)
            {
                _highScore = Glider.Score;
                SaveValues();
                MainView.HighScoreView.OpenHighScoreView(true,_highScore);
                MainView.StartScreenView.OpenScoreButton.onClick.Invoke();
            }

            MainView.StartScreenView.PlayButton.SetStateToNotPlaying();
            MainView.StartScreenView.PlayAnimation();
            MainView.StartScreenView.SetHighScore(_highScore);
            MainView.StartScreenView.SetColors(_color);

            MainView.SetHighScoreButtonState(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerAlias")));
            

            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (MainView.StartScreenView.PlayButton.GetState())
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

        private enum BoostState
        {
            None,
            StartingBoost,
            Boosted,
            StoppingBoost,
        }

        private void SetSpeeds(bool isBoosted)
        {
            var score = Glider.Score;

            CirclesView.SetSpeed(score, isBoosted);
            DiamondsView.SetSpeed(score, isBoosted);
            PowerupsView.SetSpeed(score, isBoosted);
        }
    }
}
