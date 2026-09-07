using System;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class HealthManager : MonoSingleton<HealthManager>
    {
        public UnityEvent OnDeath;
        public UnityEvent<int> OnHealthChange;

        [SerializeField] private int maxHealth = 3;
        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                _health = Mathf.Clamp(_health, 0, maxHealth);
                OnHealthChange?.Invoke(_health);
                if (_health <= 0)
                {
                    OnDeath?.Invoke();
                    Debug.Log("게임 오버");
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}