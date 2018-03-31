using System;
using Assets.Scripts.Money;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupItemShopModel
    {
        private ItemType _itemType;
        private double _baseCost = 100;
        private int _level;

        private readonly string _name;

        public PowerupItemShopModel(ItemType type)
        {
            _name = type.ToString();
            _level = PlayerPrefs.GetInt(type.ToString());
        }

        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                PlayerPrefs.SetInt(Name,value);
            }
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
            Level++;
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

    public enum ItemType
    {
        Shield,
        SpeedBoost,
    }
}