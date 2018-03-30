using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScoreEntryView : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;

        [SerializeField] private Image _box;

        public void UpdateDescription(string playerName, string score, bool highScoreIsPlayer)
        {
            _name.text = playerName;
            _score.text = score;

            _box.color = highScoreIsPlayer ? Color.red : Color.white;
        }
    }
}