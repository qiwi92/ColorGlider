

using UnityEngine;

namespace Highscore
{
    public class HighScoreFactory : MonoBehaviour
    {
        [SerializeField] private HighscoreView _highscoreView;

        private void Start()
        {
            var model = new HighScoreModel();
            _highscoreView.Initialize(model);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var highScore = Random.Range(10, 150);
                _highscoreView.OpenHighScoreView(true, highScore);
            }

        }
    }
}