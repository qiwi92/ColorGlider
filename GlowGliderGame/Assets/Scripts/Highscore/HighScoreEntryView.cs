using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScoreEntryView : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;
        [SerializeField] private Text _rank;

        public void UpdateDescription(string playerName, int score,int rank, bool highScoreIsPlayer)
        {
            _name.text = playerName;
            _score.text = score.ToString();
            _rank.text = rank + ".";

            var col = _name.color;
            col.a = highScoreIsPlayer ? 1 : 0.5f;

            _name.color = col;
            _score.color = col;
            _rank.color = col;
        }
    }
}