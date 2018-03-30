using UnityEngine;

namespace Money
{
    public class MoneyService
    {
        private static MoneyService _instance;
        public static MoneyService Instance
        {
            get { return _instance ?? (_instance = new MoneyService()); }
        }

        private int _money;

        public int CurrentMoney
        {
            get { return _money; }
        }

        public void AddMoney(int amount)
        {
            _money += amount;
        }

        public void SubtractMoney(int amount)
        {
            if (_money - amount < 0)
            {
                Debug.LogWarning("Subtracted more money than available");
                _money = 0;
            }

            _money -= amount;
        }
    }
}