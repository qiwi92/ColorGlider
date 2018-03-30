using UnityEngine;
using UnityEngine.UI;

namespace Highscore
{
    public class HighScoreEntryView : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;

        public void UpdateDescription(string playerName, string score)
        {
            _name.text = playerName;
            _score.text = score;
        }
    }
}