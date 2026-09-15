using System;
using System.Collections.Generic;
using UnityEngine;

namespace _01.Scripts.Upgrade
{
    [Serializable]
    public struct UpgradeData
    {
        public string Id;
        public string DisplayName;
        public BigNumber InitialValue;
        public double ValueMultiplier; 
        public BigNumber InitialCost;
        public double CostMultiplier;
        [HideInInspector] public int CurrentLevel;
        public int MaxLevel;
        public List<string> Prerequisites;

        public UpgradeData(string id, string displayName, int maxLevel, int currentLevel, BigNumber initialValue, BigNumber initialCost, double costMultiplier = 1.5,
            double valueMultiplier = 1.5)
        {
            Id = id;
            DisplayName = displayName;
            InitialValue = initialValue;
            ValueMultiplier = valueMultiplier;
            InitialCost = initialCost;
            CostMultiplier = costMultiplier;
            CurrentLevel = 0;
            MaxLevel = maxLevel;
            Prerequisites = new List<string>();
        }

        public BigNumber CalculateCost(int targetLevel) //업그레이드 필요한 가격
		{
			if (InitialCost.Mantissa == 0) return BigNumber.Zero;
			double power = Math.Log10(CostMultiplier) * Math.Max(0, targetLevel);
			long exponent = (long)Math.Floor(power);
			return InitialCost * new BigNumber(Math.Pow(10, power - exponent), exponent);
		}
    }
}