namespace UI.HighScore
{
    public interface ICharacterSelectorViewModel
    {
        void IncrementCharacter();
        void DecrementCharacter();
        string Character { get; }
    }
}