using System.ComponentModel;
using Assets.Scripts.Money;

public partial class SROptions
{
    private const string MoneyCategory = "Money";

    [Category(MoneyCategory)]
    public void RichBitch()
    {
        MoneyService.Instance.AddMoney(10000000);
        OnPropertyChanged("CurrentCash");
    }

    [Category(MoneyCategory)]
    public void PoorBastard()
    {
        MoneyService.Instance.SubtractMoney(MoneyService.Instance.CurrentMoney);
        OnPropertyChanged("CurrentCash");
    }

    [Category(MoneyCategory)]
    public string CurrentCash
    {
        get { return MoneyService.Instance.CurrentMoney.ToString(); }
    }
}