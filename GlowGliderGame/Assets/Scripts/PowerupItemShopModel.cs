using System;
using Assets.Scripts.Money;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupItemShopModel
    {
        private readonly ItemType _itemType;
        private readonly IPowerupData _powerupData;
        private double _baseCost = 100;
        private int _level;

        public PowerupItemShopModel(ItemType type, IPowerupData powerupData)
        {
            _itemType = type;
            _powerupData = powerupData;
            Name = powerupData.GetName();
            _level = PlayerPrefs.GetInt(type.ToString());
        }

        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                PlayerPrefs.SetInt(_itemType.ToString(),value);
            }
        }

        public string Name { get; }

        public void TryBuy()
        {
            if (!CanBuy())
            {
                throw new Exception("bullshit");
            }

            MoneyService.Instance.SubtractMoney((int)Cost);
            Level++;
        }

        public ItemType Type => _itemType;

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
            get { return _powerupData.GetCost(_level); }
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