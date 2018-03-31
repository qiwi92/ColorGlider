namespace Assets.Scripts.Money
{
    public interface IObserver
    {
        void NotifyChange(int value);
    }
}