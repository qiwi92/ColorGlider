using System;
using Assets.Scripts.Money;
using Assets.Scripts.Powerups;
using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupItemShopModel
    {
        private readonly PowerupType _powerupType;
        private readonly IPowerupData _powerupData;
        private double _baseCost = 100;
        private int _level;

        public PowerupItemShopModel(PowerupType type, IPowerupData powerupData)
        {
            _powerupType = type;
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
                PlayerPrefs.SetInt(_powerupType.ToString(),value);
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

        public PowerupType Type => _powerupType;
        public bool IsMaxLevel => _level >= MaxLevel;
        public bool IsUnlocked => _level > 0;
        public double Cost => _powerupData.GetCost(_level);
        public int MaxLevel => 10;

        public bool CanBuy()
        {
            return MoneyService.Instance.CurrentMoney >= Cost && !IsMaxLevel;
        }     
    }
}