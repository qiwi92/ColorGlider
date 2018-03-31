

using UnityEngine;

namespace Highscore
{
    public class HighScoreFactory : MonoBehaviour
    {
        [SerializeField] private HighScoreView _highScoreView;

        private void Start()
        {
            var model = new HighScoreModel();
            _highScoreView.Initialize(model);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var highScore = Random.Range(10, 150);
                _highScoreView.OpenHighScoreView(true, highScore);
            }

        }
    }
}