using System;
using UnityEngine;

namespace Highscore
{
    public class HighScoreFactory : MonoBehaviour
    {
        [SerializeField] private HighscoreView _highscoreView;
        [SerializeField] private DebugEnterHighScoreView _debugEnterHighScoreView;

        private void Start()
        {
            var model = new HighScoreModel();
            _highscoreView.Initialize(model);

            if (Debug.isDebugBuild && _debugEnterHighScoreView.enabled)
            {
                _debugEnterHighScoreView.Initialize(model);
            }
            else
            {
                _debugEnterHighScoreView.enabled = false;
            }
        }
    }
}