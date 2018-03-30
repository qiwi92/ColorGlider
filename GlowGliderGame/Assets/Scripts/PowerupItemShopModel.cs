using System;
using Money;

namespace Assets.Scripts
{
    public class PowerupItemShopModel
    {
        private double _baseCost = 100;
        private int _level;

        private string _name;

        public PowerupItemShopModel(int level, string name)
        {
            _level = level;
            _name = name;
        }

        public int Level
        {
            get { return _level; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void TryBuy()
        {
            if (!CanBuy())
            {
                throw new Exception("bullshit");
            }

            MoneyService.Instance.SubtractMoney((int)Cost);
            _level++;
        }

        public bool IsMaxLevel
        {
            get { return _level >= MaxLevel; }
        }

        public bool IsUnlocked
        {
            get { return _level > 0; }
        }

        public double Cost
        {
            get { return _baseCost * (_level + 1); }
        }

        public int MaxLevel
        {
            get { return 10; }
        }

        public bool CanBuy()
        {
            return MoneyService.Instance.CurrentMoney >= Cost && !IsMaxLevel;
        }     
    }
}