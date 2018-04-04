using Highscore;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private MainView _mainView;

        [SerializeField] private Camera _camera;

        [SerializeField] private PowerupShopView _shopViewPrefab;
        [SerializeField] private HighScorePanelView _highScoreViewPrefab;

        private void Awake()
        {
            var uiRoot = new GameObject("Ui Root").transform;

            var shopview = Instantiate(_shopViewPrefab, uiRoot);
            shopview.SetupCamera(_camera);

            var highscoreView = Instantiate(_highScoreViewPrefab, uiRoot);
            _mainView.Setup(highscoreView, shopview);
            highscoreView.SetupCamera(_camera);

            _gameManager.InitializeGame(_mainView);
        }
    }
}