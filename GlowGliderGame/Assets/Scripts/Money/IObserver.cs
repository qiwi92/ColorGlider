namespace Money
{
    public interface IObserver
    {
        void NotifyChange(int value);
    }
}