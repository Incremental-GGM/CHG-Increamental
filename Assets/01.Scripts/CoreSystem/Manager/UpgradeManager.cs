using System;
using System.Collections.Generic;
using System.Linq;
using _01.Scripts.Upgrade;
using Manager;
using Unity.Mathematics;
using UnityEngine;
using Upgrade;

namespace _01.Scripts.CoreSystem.Manager
{
    public class UpgradeManager : MonoSingleton<UpgradeManager>
    {
        public event Action<UpgradeData> OnStatChanged;
        private Dictionary<string, UpgradeData> _upgradeDataDict = new Dictionary<string, UpgradeData>();
        
        [SerializeField] private List<UpgradeData> _defaultUpgradeData;

        protected override void Awake()
        {
            base.Awake();
            _upgradeDataDict = _defaultUpgradeData.ToDictionary(k => k.Id, v =>
            {
                OnStatChanged?.Invoke(v);
                return v;
            });
        }

        public bool TryUpgrade(string key)
        {
            if (_upgradeDataDict.TryGetValue(key, out var data))
            {
                if (!GoldManager.Instance.TryBuy(data.CalculateCost(data.CurrentLevel)) || data.MaxLevel <= data.CurrentLevel) return false;
                if (data.Prerequisites != null) //선행 조건 있다면 체크
                {
                    foreach (string prerequisite in data.Prerequisites)
                        if (!_upgradeDataDict.ContainsKey(prerequisite) || _upgradeDataDict[prerequisite].CurrentLevel <= 0)
                            return false;
                    
                }

                data.CurrentLevel++;
                double cost = Math.Log10(data.CostMultiplier) * data.CurrentLevel;
                long exponent = (long)Math.Floor(cost);
                double mantissa = Math.Pow(10,cost - exponent);
                data.InitialCost += new BigNumber(mantissa, exponent);

                double value = Math.Log10(data.ValueMultiplier) * data.CurrentLevel;
                long valueExponent = (long)Math.Floor(value);
                double valueMantissa = Math.Pow(10, value - valueExponent);
                data.InitialValue += new BigNumber(valueMantissa, valueExponent);

                _upgradeDataDict[key] = data;
				OnStatChanged?.Invoke(data);
                return true;
            }

            return false;
        }

        public UpgradeData GetUpgradeData(string key)
        {
            if (_upgradeDataDict.TryGetValue(key, out var data))
                return data;
            else return new UpgradeData();
        }
    }
}