namespace UI.HighScore
{
    public class CharacterSelectorViewModel : ICharacterSelectorViewModel
    {
        public string Character => Characters[_currentIdx];

        private static readonly string[] Characters = 
        {
            "-", "A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J", "K",
            "L", "M", "N", "O", "P", "Q",
            "R", "S", "T", "U", "V", "W",
            "X", "Y", "Z"
        };

        private int _currentIdx;

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
            if (value < 0) value = Characters.Length - 1;
            if (value >= Characters.Length) value = value % Characters.Length;

            return value;
        }
    }
}