using System.Collections.Generic;
using UnityEngine;

namespace Money
{
    public class MoneyService
    {
        private static MoneyService _instance;
        public static MoneyService Instance => _instance ?? (_instance = new MoneyService());

        private readonly List<IObserver> _observables;

        private int _money;
        private int Money
        {
            get { return _money; }
            set
            {
                _money = value;
                PlayerPrefs.SetInt("Money",value);
                NotifyObservers(value);
            }
        }

        private MoneyService()
        {
            _observables = new List<IObserver>();
            SetMoneyFromSaveGame();
        }

        public int CurrentMoney => Money;

        public void AddMoney(int amount)
        {
            Money += amount;
        }

        public void SubtractMoney(int amount)
        {
            if (Money - amount < 0)
            {
                Debug.LogWarning("Subtracted more money than available");
                Money = 0;
            }

            Money -= amount;
        }

        private void SetMoneyFromSaveGame()
        {
            Money = PlayerPrefs.GetInt("Money");
        }

        public void AddObserver(IObserver observer)
        {
            _observables.Add(observer);
        }

        private void NotifyObservers(int value)
        {
            foreach (var observer in _observables)
            {
                observer.NotifyChange(value);
            }
        }
    }
}