﻿using System;
using System.Net;
using Analytics;
using Assets.Scripts.Money;
using Assets.Scripts.Powerups;
using DG.Tweening;
using Highscore;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private MainView _mainView;

        public InputController InputController;
        public ColorPalette ColorPalette;
        public CirclesView CirclesView;
        public DiamondsView DiamondsView;
        public PowerupsView PowerupsView;
        public Glider Glider;

        public ScoreView ScoreView;

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

        public void InitializeGame(MainView mainView)
        {
            _mainView = mainView;

            if (Debug.isDebugBuild)
               SRDebug.Init();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            ServicePointManager.ServerCertificateValidationCallback = ServerUtils.MyRemoteCertificateValidationCallback;

            LoadValues();

            mainView.StartScreenView.SetHighScore(_highScore);

            _boostState = BoostState.None;
            _state = GameState.Init;
            CirclesView.ColorPalette = ColorPalette;
            Glider.ColorPalette = ColorPalette;
            Glider.Sounds = Sounds;

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

            _mainView.SetHighScoreButtonState(HighScoreUnlocked());
            _mainView.SetShopButtonState(ShopUnlocked());

            Setup();
        }


        private bool HighScoreUnlocked()
        {
            return _highScore > 15;
        }

        private bool ShopUnlocked()
        {
            if (SavegameService.Instance.HasUnlockedShop)
                return true;

            var shouldUnlock = MoneyService.Instance.CurrentMoney >= 20;
            SavegameService.Instance.HasUnlockedShop = shouldUnlock;
            return shouldUnlock;
        }

        private void Setup()
        {
            Glider.IsAlive = false;
            Glider.ResetPosition();
            _mainView.SetColors(_color);
            SetColors();

            Sounds.Setup();
            Sounds.PlayDeathTheme(true);

            Graphics.activeTier = (GraphicsTier) 1;

            if (SavegameService.Instance.SoundIsOn)
            {
                _mainView.StartScreenView.ToggleSound.isOn = true;
            }
            else
            {
                _mainView.StartScreenView.ToggleSound.isOn = false;
                Sounds.Mute();
            }

            var toggle = _mainView.StartScreenView.ToggleSound;
            toggle.onValueChanged.AddListener(delegate
            {
                if (toggle.isOn)
                {
                    Sounds.Unmute();
                    SavegameService.Instance.SoundIsOn = true;
                }
                else
                {
                    Sounds.Mute();
                    SavegameService.Instance.SoundIsOn = false;
                }
            });
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
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (_mainView.StartScreenView.PlayButton.GetState())
            {
                _state = GameState.Starting;
            }
        }

        private void HandleStartingState()
        {
            _gameDuration = 0;
            _mainView.StartScreenView.PlayAnimation();
            _mainView.DeactivateShopCanvas();
            _mainView.DeactivateScoreCanvas();

            Glider.Score = SROptions.StartScoreOption;        
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
            InputController.Move(Glider.transform,Glider.Height,_screenWidth,Glider.GetSpeed());

            if (Glider.CollisionState == CollisionStates.JustCollided)
            {
                ScoreView.SetScore(Glider.Score);
                CirclesView.SetSpeed(Glider.Score,Glider.IsBoosted);
                DiamondsView.SetSpeed(Glider.Score, Glider.IsBoosted);
                PowerupsView.SetSpeed(Glider.Score, Glider.IsBoosted);
                SetColors();


                if (!Glider.IsBoosted)
                {
                    SetCounterDots();
                    _mainView.PanelColorChange(_color, Glider.Index);
                }

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
            _analyticsApi.TrackSession((ushort) _gameDuration, _guidProvider.GetGuid().ToString(),(ushort)Glider.Score);
            _mainView.ActivateShopCanvas();
            _mainView.ActivateScoreCanvas();
            Glider.ResetPositionSmooth();
            
            PanelImage.DOFade(0.8f, 0.2f);

            Sounds.PlayMainTheme(false);
            Sounds.PlayDeathSfx();
            Sounds.PlayDeathTheme(true);

            if (Glider.Score > _highScore)
            {
                _highScore = Glider.Score;
                SaveValues();
                _mainView.TryOpenScore(HighScoreUnlocked());
                _mainView.HighScoreView.OpenHighScoreView(true,_highScore);
            }

            _mainView.StartScreenView.PlayButton.SetStateToNotPlaying();
            _mainView.StartScreenView.PlayAnimation();
            _mainView.StartScreenView.SetHighScore(_highScore);
            _mainView.SetColors(_color);

            _mainView.SetHighScoreButtonState(HighScoreUnlocked());
            _mainView.SetShopButtonState(ShopUnlocked());


            _state = GameState.Dead;
        }

        private void HandleDeadState()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (_mainView.StartScreenView.PlayButton.GetState())
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

        private void LoadValues()
        {
            _highScore = SavegameService.Instance.HighScore;
        }

        private void SaveValues()
        {
            SavegameService.Instance.HighScore = _highScore;
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