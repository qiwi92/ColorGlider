using System.Collections.Generic;

namespace UI.HighScore
{
    public class CharacterSelectorViewModel : ICharacterSelectorViewModel
    {
        private int _currentIdx;

        public string Character
        {
            get { return Characters[_currentIdx]; }
            set { _currentIdx = Characters.IndexOf(value); }
        }

        private static readonly List<string> Characters = new List<string>
        {
            "-", "A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J", "K",
            "L", "M", "N", "O", "P", "Q",
            "R", "S", "T", "U", "V", "W",
            "X", "Y", "Z"
        };

        public void IncrementCharacter()
        {
            _currentIdx = WrapValue(_currentIdx + 1);
        }

        public void DecrementCharacter()
        {
            _currentIdx = WrapValue(_currentIdx - 1);
        }

        private static int WrapValue(int value)
        {
            if (value < 0) value = Characters.Count - 1;
            if (value >= Characters.Count) value = value % Characters.Count;

            return value;
        }
    }
}