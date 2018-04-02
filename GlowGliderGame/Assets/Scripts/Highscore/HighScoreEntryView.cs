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

        public void UpdateDescription(string playerName, int score,int rank, bool highScoreIsPlayer)
        {
            _name.text = playerName;
            _score.text = score.ToString();
            _rank.text = rank + ".";

            var playerEntryColor = _colorPalette.Colors[1];
            playerEntryColor.a = 0.5f;

            playerEntryColor = highScoreIsPlayer ? playerEntryColor : _colorPalette.AlphaWhite;


            _backgroundImage.color = playerEntryColor;
            _name.color = _colorPalette.Colors[1];
            _score.color = _colorPalette.Colors[0];
            _rank.color = _colorPalette.Colors[2];
        }
    }
}