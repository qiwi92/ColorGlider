using Money;

public partial class SROptions
{
    public void RichBitch()
    {
        MoneyService.Instance.AddMoney(10000000);
    }

    public string CurrentCash
    {
        get { return MoneyService.Instance.CurrentMoney.ToString(); }
    }
}