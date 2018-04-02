using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScoreEntryView : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;
        [SerializeField] private Text _rank;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private ColorPalette _colorPalette;

        public void UpdateDescription(PlayerHighScore highScore)
        {
            _name.text = highScore.PlayerName;
            _score.text = highScore.Score.ToString();
            _rank.text = highScore.Rank + ".";

            var playerEntryColor = _colorPalette.Colors[1];
            playerEntryColor.a = 0.5f;

            playerEntryColor = highScore.IsPlayer 
                ? playerEntryColor 
                : _colorPalette.AlphaWhite;
            
            _backgroundImage.color = playerEntryColor;
            _name.color = _colorPalette.Colors[1];
            _score.color = _colorPalette.Colors[0];
            _rank.color = _colorPalette.Colors[2];
        }

        public void Disable()
        {
            _name.text = string.Empty;
            _score.text = string.Empty;
            _rank.text = string.Empty;
        }
    }
}